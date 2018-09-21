using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimation : MonoBehaviour {

	// 今のポジション（実座標ではなく、グリッド上で左上を0，0とした時の、何行何列目かを示す。）
	public Vector2Int nowPos;
	public Vector2Int targetPos;

	private float moveSpeed = 0.05f;

	private GridInfo gridInfo;   // 参照用
	
	public bool arrived = false;  // 移動や結合が終わった状態かどうか
	
	void Start () {
		gridInfo = transform.parent.GetComponent<GridInfo>();

		// デバッグ用
		//SetStartPos(2, 2);
		targetPos = new Vector2Int(2, 4);
	}

	void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			SetStartPos(2, 5);
			targetPos = new Vector2Int(2, 5);
		}

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

			transform.position = new Vector3(transform.position.x + (targetDir.x * moveSpeed), transform.position.y + (targetDir.y * moveSpeed));

			//print("distance = " + Vector2.Distance(gridInfo.centerCoordinate[targetPos.x, targetPos.y], transform.position));
			if (Vector2.Distance(gridInfo.centerCoordinate[targetPos.x, targetPos.y], transform.position) <= 0.05f)
			{
				nowPos = targetPos;
			}
		}
		else
		{
			// 目標地点に到着しているときの処理
			// まだ上に落ちれるなら落ちて、そうでなければ周りと合体できるかチェック
			DropCheck();

			if (arrived)
			{
				MergeCheck();
			}
		}

		

	}

	// 削除するときに呼び出す
	public void Delete()
	{
		
	}

	// 周りと合体できるかチェックする
	public void MergeCheck()
	{
		// 4方向のチェック
		for(int x = -1; x <= 1; x += 2)
		{
			for (int y = -1; y <= 1; y += 2)
			{
				try{
					// 指定したマスに合体できるブロックがあるかどうかの判定(今は全部合体するようになっています)
					if(gridInfo.monsterPos[nowPos.x + x, nowPos.y + y] != null)
					{
						gridInfo.monsterPos[nowPos.x + x, nowPos.y + y].GetComponent<BlockAnimation>().targetPos = nowPos;
						gridInfo.monsterPos[nowPos.x + x, nowPos.y + y].GetComponent<BlockAnimation>().Delete();
					}
				}catch{
					// 枠をはみ出て探索することを防ぐためのtry-catch
				}
			}
		}
	}

	// ブロックの初期位置を指定
	public void SetStartPos(int x, int y)
	{
		nowPos = new Vector2Int(x, y);
		transform.position = gridInfo.centerCoordinate[x, y];
	}

	// 自分より上にブロックが無ければ上に詰める
	public void DropCheck()
	{
		try
		{
			if (gridInfo.monsterPos[nowPos.x, nowPos.y - 1] == null)
			{
				targetPos = new Vector2Int(nowPos.x, nowPos.y - 1);
				gridInfo.monsterPos[nowPos.x, nowPos.y] = null;
				gridInfo.monsterPos[nowPos.x, nowPos.y - 1] = this.gameObject;
				return;
			}else{
				arrived = true;
			}
		}catch{ 
			arrived = true; 
		}
	}
}
