using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBlocksController : MonoBehaviour
{

    // Use this for initialization

    private GameObject nextBlocksArea;
    private readonly Vector2[] nextBlocksPosition =
        new Vector2[]
        {
            new Vector2(-2, 0),
            new Vector2(-2, 1),
            new Vector2(-2, 2),
            new Vector2(-2, 3)
        };
    private GameObject blockPrefab;

    void Start()
    {
        nextBlocksArea = GameObject.Find("NextBlocksArea");
        blockPrefab = (GameObject)Resources.Load("Block");

        for (var i = 0; i < nextBlocksPosition.Length; i++)
        {
            var block = GenerateBlockGameObject();
            block.transform.position = nextBlocksPosition[i];
            block.GetComponent<SpriteRenderer>().sortingOrder = 1;
            block.GetComponent<BlockImageManager>().ImageReload();
        }
    }


    private GameObject GenerateBlockGameObject()
    {
        var go = Instantiate(blockPrefab, Vector2.zero, Quaternion.identity);
        go.transform.parent = nextBlocksArea.transform;
        return go;
    }


    // NextBlockを最大限まで生成&規定位置移動
    private void UpdateNextBlock()
    {
        var block = GenerateBlockGameObject();
        block.GetComponent<SpriteRenderer>().sortingOrder = 1;
        block.GetComponent<BlockImageManager>().ImageReload();
        Debug.Log(block.GetComponent<Block>().blockData);

        // i=0 はDestroyするObjectなのでi=1にする
        for (var i = 1; i < this.transform.childCount; i++)
        {
            var go = this.transform.GetChild(i).gameObject;
            go.transform.position = nextBlocksPosition[i-1];
            go.GetComponent<BlockImageManager>().ImageReload();
        }
    }

    public GameObject SendNextBlock()
    {
        var nextBlock = this.transform.GetChild(0).gameObject;
        var nextBlockClone = (GameObject)Object.Instantiate(nextBlock);

        //親子関係の問題でDestoy前にUpdateNextBlockを呼ぶ
        UpdateNextBlock();
        Destroy(nextBlock);

        return nextBlockClone;
    }
}
