using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimation : MonoBehaviour
{

    // 今のポジション（実座標ではなく、グリッド上で左上を0，0とした時の、何行何列目かを示す。）
    public Vector2Int nowPos;
    public Vector2Int targetPos;

    private float moveSpeed = 0.05f;

    private GridInfo gridInfo;   // 参照用

    [SerializeField] private bool isArrived = false;  // 移動や結合が終わった状態かどうか
    private bool isMerged = false;  // 合体処理が終わった状態かどうか

    public bool IsArrived { get; private set; }
    public bool IsMerged { get; private set; }

    private bool deleteFlag = false; // 合体中で、移動が終わり次第削除すべきかどうか

    void Start()
    {
        gridInfo = transform.parent.GetComponent<GridInfo>();

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

            transform.position = new Vector3(transform.position.x + (targetDir.x * moveSpeed), transform.position.y + (targetDir.y * moveSpeed));

            //print("distance = " + Vector2.Distance(gridInfo.centerCoordinate[targetPos.x, targetPos.y], transform.position));
            if (Vector2.Distance(gridInfo.centerCoordinate[targetPos.x, targetPos.y], transform.position) <= 0.05f)
            {
                if (deleteFlag)
                {
                    gridInfo.monsterPos[nowPos.x, nowPos.y] = null;
                    Destroy(this.gameObject);
                }
                nowPos = targetPos;
            }
        }
        else
        {
            // 目標地点に到着しているときの処理
            // まだ上に落ちれるなら落ちて、そうでなければ周りと合体できるかチェック
            DropCheck();
            //print("nowPos = " + nowPos + "\tarrived = " + arrived);

            if (isArrived == true && isMerged == false)
            {
                MergeCheck();
                isMerged = true;
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
        // 4方向のチェック
        for (int x = -1; x <= 1; x += 2)
        {
            try
            {
                //print("checked monster info : " + gridInfo.monsterPos[nowPos.x + x, nowPos.y]);
                // 指定したマスに合体できるブロックがあるかどうかの判定(今は全部合体するようになっています)
                if (gridInfo.monsterPos[nowPos.x + x, nowPos.y] != null)
                {
                    gridInfo.monsterPos[nowPos.x + x, nowPos.y].GetComponent<BlockAnimation>().targetPos = nowPos;
                    gridInfo.monsterPos[nowPos.x + x, nowPos.y].GetComponent<BlockAnimation>().Delete();
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
                if (gridInfo.monsterPos[nowPos.x, nowPos.y + y] != null)
                {
                    gridInfo.monsterPos[nowPos.x, nowPos.y + y].GetComponent<BlockAnimation>().targetPos = nowPos;
                    gridInfo.monsterPos[nowPos.x, nowPos.y + y].GetComponent<BlockAnimation>().Delete();
                }
            }
            catch
            {
                // 枠をはみ出て探索することを防ぐためのtry-catch
            }
        }
    }

    // ブロックの初期位置を指定
    public void SetStartPos(int x, int y)
    {
        if (gridInfo == null)
        {
            gridInfo = GameObject.Find("BlockArea").GetComponent<GridInfo>();
        }
        //print("gridInfo : " + gridInfo);
        nowPos = new Vector2Int(x, y);
        transform.position = gridInfo.centerCoordinate[x, y];
        DropCheck();
        //targetPos = nowPos;
    }

    // 自分より上にブロックが無ければ上に詰める
    private void DropCheck()
    {
        if (nowPos.y - 1 < 0)
        {
            // 最上部到達
            isArrived = true;
        }
        else if (gridInfo.monsterPos[nowPos.x, nowPos.y - 1] != null)
        {
            // ブロック到達
            isArrived = true;
        }
        else
        {
            isArrived = false;
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
