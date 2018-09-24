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
            new Vector2(-0.06f, -4.74f),
            new Vector2(-0.88f, -4.74f),
            new Vector2(-1.7f, -4.74f),
            new Vector2(-2.52f, -4.74f)
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


    // NextBlockを生成&規定位置移動
    private void UpdateNextBlock()
    {
        var block = GenerateBlockGameObject();
        block.GetComponent<SpriteRenderer>().sortingOrder = 1;
        block.GetComponent<BlockImageManager>().ImageReload();
        block.transform.position = nextBlocksPosition[this.transform.childCount - 2];

        // i=0 はShoot対象なのでi=1開始
        for (var i = 1; i < this.transform.childCount; i++)
        {
            var go = this.transform.GetChild(i).gameObject;
            if (go.GetComponent<NextBlockAnimation>() == null)
            {
                go.AddComponent<NextBlockAnimation>();
            }
            go.GetComponent<NextBlockAnimation>().TargetPosition = nextBlocksPosition[i - 1];
            go.GetComponent<BlockImageManager>().ImageReload();
        }
    }

    public void SetNextBlockInShootingArea()
    {
        var nextBlock = this.transform.GetChild(0).gameObject;
        UpdateNextBlock();
        nextBlock.transform.parent = GameObject.Find("ShootingArea").transform;
    }
}
