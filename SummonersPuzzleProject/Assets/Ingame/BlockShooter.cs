using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * タッチの入力があったときに、BlockGeneratorから次のブロックを受け取って、
 * タッチ座標にブロックを追従させて、離したポイントによってブロックの初期位置を決定する。
 * そして、ブロックの動きを始めるメソッドを呼び出す。
 * </summary>
*/

public class BlockShooter : MonoBehaviour
{

    [SerializeField] GameObject col1;
    [SerializeField] GameObject col2;
    [SerializeField] GameObject col3;
    [SerializeField] GameObject col4;
    [SerializeField] GameObject col5;

    private List<GameObject> cols;
    private GameObject blockArea;
    private GameObject blockPrefab;
    private GameObject newBlock;

    void Start()
    {
        cols = new List<GameObject>();
        cols.Add(col1);
        cols.Add(col2);
        cols.Add(col3);
        cols.Add(col4);
        cols.Add(col5);
        blockArea = GameObject.Find("BlockArea");
        blockPrefab = (GameObject)Resources.Load("Block");
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit)
            {
                Bounds rect = hit.collider.bounds;
                if (rect.Contains(worldPoint))
                {
                    var neighborCol = hit.collider.gameObject;

                    if (newBlock == null)
                    {
                        // 掴んだ時
                        newBlock = GenerateBlockGameObject(worldPoint);
                    }

                    SetBlockPosition(neighborCol);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            // Debug.Log("離した");
            newBlock = null;
        }
    }

    private void SetBlockPosition(GameObject neighborCol)
    {
        var index = cols.IndexOf(neighborCol);
        Vector2 pos = Vector2.zero;
        switch (index)
        {
            case 0:
                pos = new Vector2(-1.86f, -4.53f);
                break;
            case 1:
                pos = new Vector2(-0.83f, -4.53f);
                break;
            case 2:
                pos = new Vector2(0.13f, -4.53f);
                break;
            case 3:
                pos = new Vector2(1.16f, -4.53f);
                break;
            case 4:
                pos = new Vector2(2.1f, -4.53f);
                break;
            default:
                throw new ArgumentException("None");
        }
        newBlock.transform.position = pos;
		newBlock.GetComponent<BlockAnimation>().SetStartPos(index, 5);
	}

    private GameObject GenerateBlockGameObject(Vector2 worldPoint)
    {
        var go = Instantiate(blockPrefab, new Vector3(worldPoint.x, worldPoint.y, 0), Quaternion.identity);
        go.transform.parent = blockArea.transform;
        return go;
    }
}


