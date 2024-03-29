﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimation : MonoBehaviour
{

	// 今のポジション（実座標ではなく、グリッド上で左上を0，0とした時の、何行何列目かを示す。）
	public Vector2Int nowPos;
	public Vector2Int targetPos;

	private float moveSpeed = 20f;

	private GridInfo gridInfo;   // 参照用
	private ForceManager forceManager;

	//[SerializeField] private bool isArrived = false;
	//[SerializeField] private bool isMerged = false;
	public bool IsArrived { get; private set; }  // 移動や結合が終わった状態かどうか
	public bool IsMerged { get; private set; }   // 合体処理が終わった状態かどうか

	private bool deleteFlag = false; // 合体中で、移動が終わり次第削除すべきかどうか


	void Start()
	{
		gridInfo = transform.parent.GetComponent<GridInfo>();
		forceManager = GameObject.Find("ForceManager").GetComponent<ForceManager>();
		
		// デバッグ用
		//SetStartPos(2, 2);
		//targetPos = new Vector2Int(2, 4);
	}

	void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			// スペースキーを押すと、monsterPosの配列の中身を出力する
			print(Arr2StrEncode(gridInfo.monsterPos));
		}

		//print("nowPos:" + nowPos + "\ttargetPos:" + targetPos);
		if (nowPos.x != targetPos.x || nowPos.y != targetPos.y)
		{
			// 今の位置と目標地点が違った場合の処理（移動する）
			Vector2Int targetDir = (targetPos - nowPos);
			//print("targetDirOld : " + targetDir);
			//targetDir = new Vector2Int(targetDir.x % 1, targetDir.y % 1);
			if (targetDir.x != 0) targetDir.x /= Mathf.Abs(targetDir.x);
			if (targetDir.y != 0) targetDir.y /= Mathf.Abs(targetDir.y);
			targetDir.y *= -1;     // 要素番号がマイナス　→　座標値としてはプラス方向
			//print("targetDir : " + targetDir);

			// 直前の「現在地と目的地の距離」を保持し、もし移動後に広がっていたら通り過ぎていることを判定する
			float preDistance = Vector2.Distance(gridInfo.centerCoordinate[targetPos.x, targetPos.y], transform.position);
			Vector3 nextPos = new Vector3(transform.position.x + (targetDir.x * moveSpeed * Time.deltaTime), transform.position.y + (targetDir.y * moveSpeed * Time.deltaTime));
			float nextDistance = Vector2.Distance(gridInfo.centerCoordinate[targetPos.x, targetPos.y], nextPos);
			
			//float nowDistance = Vector2.Distance(gridInfo.centerCoordinate[targetPos.x, targetPos.y], transform.position);

			//print("distance = " + Vector2.Distance(gridInfo.centerCoordinate[targetPos.x, targetPos.y], transform.position));
			//if (Vector2.Distance(gridInfo.centerCoordinate[targetPos.x, targetPos.y], transform.position) <= 0.1f)
			if (preDistance < nextDistance)
			{
				//transform.position = gridInfo.centerCoordinate[targetPos.x, targetPos.y];
				if (deleteFlag)
				{
					GameObject.Find("SoundManager").GetComponent<SEManager>().PlaySE("merge", 0.5f);
					//gridInfo.monsterPos[targetPos.x, targetPos.y].GetComponent<Block>().blockData.Rank += gridInfo.monsterPos[nowPos.x, nowPos.y].GetComponent<Block>().blockData.Rank;
					if (gridInfo.monsterPos[targetPos.x, targetPos.y] != null)
					{
						// 合体先のブロックがまだ存在していた場合に実行
						// （2つ以上の合体時は、1つ目の処理により合体先のブロックが移動している場合があるため）
						gridInfo.monsterPos[targetPos.x, targetPos.y].GetComponent<BlockImageManager>().ImageReload();
						gridInfo.monsterPos[targetPos.x, targetPos.y].GetComponent<BlockAnimation>().IsArrived = false;
						gridInfo.monsterPos[targetPos.x, targetPos.y].GetComponent<BlockAnimation>().IsMerged = false;
					}
					// 元々の位置にあったオブジェクトはnullにする
					gridInfo.monsterPos[nowPos.x, nowPos.y] = null;

					// TODO: nullにした列だけIsArrivedをfalseにしたほうが処理が軽くなりそう
					// (今は、すべてのブロックが常に上に詰めれるかを判定してしまっている。)
					transform.Find("Light").GetComponent<BlockLight>().Merge();

					// 下の処理は、BlockLight.csで、波紋アニメーションが終わった後に削除するようにした
					GetComponent<SpriteRenderer>().enabled = false;
					//Destroy(this.gameObject);
				}
				nowPos = targetPos;
			}
			else{
				transform.position = nextPos;
			}
		}
		else
		{
			// 目標地点に到着しているときの処理
			// まだ上に落ちれるなら落ちて、そうでなければ周りと合体できるかチェック
			DropCheck();

			//print("nowPos = " + nowPos + "\tIsArrived = " + IsArrived + "\tIsMerged = " + IsMerged);

			if (IsArrived == true && IsMerged == false)
			{
				
				MergeCheck();
				IsMerged = true;
			}

			if (IsArrived && IsMerged && transform.localScale.x >= 0.9f)
			{
				try{  // タッチ中はエラーが出るので無視する
					transform.position = gridInfo.centerCoordinate[targetPos.x, targetPos.y];
				}catch{}
			}
		}
	}

	// 削除するときに呼び出す
	public void Delete()
	{
		deleteFlag = true;
	}

	// 周りと合体できるかチェックする(冗長な書き方のため、修正可能)
	private void MergeCheck()
	{
		int addRank = 0;
		// 4方向のチェック
		for (int x = -1; x <= 1; x += 2)
		{
			try
			{
				//print("checked monster info : " + gridInfo.monsterPos[nowPos.x + x, nowPos.y]);
				// 指定したマスに合体できるブロックがあるかどうかの判定(今は全部合体するようになっています)
				//if (gridInfo.monsterPos[nowPos.x + x, nowPos.y] != null)
				BlockData targetBlockData = gridInfo.monsterPos[nowPos.x + x, nowPos.y].GetComponent<Block>().blockData;
				if (targetBlockData.Rank == GetComponent<Block>().blockData.Rank && targetBlockData.Color.Equals(GetComponent<Block>().blockData.Color) && gridInfo.monsterPos[nowPos.x + x, nowPos.y].GetComponent<BlockAnimation>().IsArrived)
				{
					gridInfo.monsterPos[nowPos.x + x, nowPos.y].GetComponent<BlockAnimation>().targetPos = nowPos;
					gridInfo.monsterPos[nowPos.x + x, nowPos.y].GetComponent<BlockAnimation>().Delete();
					addRank += 1;
				}
			}
			catch
			{
				// 枠をはみ出て探索することを防ぐためのtry-catch
			}
		}
		for (int y = -1; y <= 1; y += 2)
		{
			try
			{
				//print("checked monster info : " + gridInfo.monsterPos[nowPos.x, nowPos.y + y]);
				// 指定したマスに合体できるブロックがあるかどうかの判定(今は全部合体するようになっています)
				//if(gridInfo.monsterPos[nowPos.x, nowPos.y + y] != null)
				BlockData targetBlockData = gridInfo.monsterPos[nowPos.x, nowPos.y + y].GetComponent<Block>().blockData;
				if (targetBlockData.Rank == GetComponent<Block>().blockData.Rank && targetBlockData.Color.Equals(GetComponent<Block>().blockData.Color) && gridInfo.monsterPos[nowPos.x, nowPos.y + y].GetComponent<BlockAnimation>().IsArrived)
				{
					gridInfo.monsterPos[nowPos.x, nowPos.y + y].GetComponent<BlockAnimation>().targetPos = nowPos;
					gridInfo.monsterPos[nowPos.x, nowPos.y + y].GetComponent<BlockAnimation>().Delete();
					addRank += 1;
				}
			}
			catch
			{
				// 枠をはみ出て探索することを防ぐためのtry-catch
			}
		}
		GetComponent<Block>().blockData.Rank += addRank;
		forceManager.AddForce((float)addRank / 10);
	}

	// ブロックの初期位置を指定
	public void SetStartPos(int x, int y)
	{
		if (gridInfo == null)
		{
			gridInfo = GameObject.Find("BlockArea").GetComponent<GridInfo>();
		}
        //print("gridInfo : " + gridInfo);

		// すべて埋まってる列に積もうとした時
		if(gridInfo.monsterPos[x, y] != null)
		{
			// 現段階では、マージ時のエフェクトを使って消すように実装
			transform.Find("Light").GetComponent<SpriteRenderer>().enabled = true;
			transform.Find("Light").GetComponent<SpriteRenderer>().sortingOrder = 3;
			transform.Find("Light").GetComponent<BlockLight>().Merge();
			GetComponent<SpriteRenderer>().enabled = false;
		}

        nowPos = new Vector2Int(x, y);
		targetPos = nowPos;

		transform.position = gridInfo.centerCoordinate[x, y];

        DropCheck();
		
		
		//print("IsArrived\t: " + IsArrived);
	}

	// 自分より上にブロックが無ければ上に詰める
	private void DropCheck()
	{
		if (nowPos.y - 1 < 0)
		{
			// 最上部到達
			if (transform.localScale.x >= 0.9f && IsArrived == false)
			{
				//print("最上部到達");
				GameObject.Find("SoundManager").GetComponent<SEManager>().PlaySE("collide", 0.5f);
			}
			IsArrived = true;
		}
		else if (gridInfo.monsterPos[nowPos.x, nowPos.y - 1] != null)
		{
			if (transform.localScale.x >= 0.9f && IsArrived == false)
			{
				//print("ブロック到達");
				GameObject.Find("SoundManager").GetComponent<SEManager>().PlaySE("collide", 0.5f);
			}
			// ブロック到達
			IsArrived = true;
			gridInfo.monsterPos[nowPos.x, nowPos.y] = this.gameObject;
		}
		else
		{
			IsArrived = false;
			IsMerged = false;
			targetPos = new Vector2Int(nowPos.x, nowPos.y - 1);
			gridInfo.monsterPos[nowPos.x, nowPos.y] = null;
			gridInfo.monsterPos[nowPos.x, nowPos.y - 1] = this.gameObject;
			//print("まだ落ちます。　nowPos:" + nowPos + "\ttargetPos:" + targetPos);
			return;
		}
	}

	// 二次元配列を文字列化
	private static string Arr2StrEncode(GameObject[,] intArr)
	{
		string str = "";
		for (int i = 0; i < intArr.GetLength(0); i++)
		{
			for (int j = 0; j < intArr.GetLength(1); j++)
			{
				if (intArr[i, j] != null)
				{
					str = str + intArr[i, j].transform.name;
				}
				else
				{
					str = str + "\t";
				}
				str = str + ","; //列の区切り文字
			}
			str = str + "\n"; //行の区切り文字
		}
		return str;
	}
}
