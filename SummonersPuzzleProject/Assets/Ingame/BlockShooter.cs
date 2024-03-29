﻿using System;
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
    private GameObject shootingBlock;

    private GameObject shootingArea;

    private NextBlocksController nextBlocksController;

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
        shootingBlock = null;
        nextBlocksController = GameObject.Find("NextBlocksArea").GetComponent<NextBlocksController>();
        shootingArea = GameObject.Find("ShootingArea");
    }

    void Update()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit)
        {
            Bounds rect = hit.collider.bounds;
            if (rect.Contains(worldPoint))
            {
                var neighborCol = hit.collider.gameObject;
                if (Input.GetMouseButton(0))
                {
                    if (newBlock == null && CanShoot())
                    {
                        // 掴んだ時
                        nextBlocksController.SetNextBlockInShootingArea();
                        newBlock = shootingArea.transform.GetChild(0).gameObject;
                        newBlock.transform.localScale = new Vector3(0.73f, 0.73f, 1);
                        Destroy(newBlock.GetComponent<NextBlockAnimation>());
                        newBlock.transform.parent = blockArea.transform;
                        shootingBlock = newBlock;
                    }

                    if (newBlock != null)
                    {
                        SetBlockPosition(neighborCol);
						newBlock.transform.Find("Light").GetComponent<BlockLight>().SetTarget();
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (newBlock != null && newBlock.GetComponent<BlockAnimation>() != null)
                    {
                        newBlock.GetComponent<BlockAnimation>().SetStartPos(cols.IndexOf(neighborCol), 5);
						newBlock.transform.Find("Light").GetComponent<SpriteRenderer>().enabled = false;
						GameObject.Find("SoundManager").GetComponent<SEManager>().PlaySE("shoot", 0.5f);
					}
                    newBlock = null;
                }
            }
        }

    }

    private void SetBlockPosition(GameObject neighborCol)
    {
        var index = cols.IndexOf(neighborCol);
        Vector2 pos = Vector2.zero;
        switch (index)
        {
            case 0:
                pos = new Vector2(-1.648f, -4f);
                break;
            case 1:
                pos = new Vector2(-0.68f, -4f);
                break;
            case 2:
                pos = new Vector2(0.29f, -4f);
                break;
            case 3:
                pos = new Vector2(1.259f, -4f);
                break;
            case 4:
                pos = new Vector2(2.228f, -4f);
                break;
            default:
                throw new ArgumentException("None");
        }
        newBlock.transform.position = pos;
        newBlock.GetComponent<SpriteRenderer>().sortingOrder = 2;
        newBlock.GetComponent<BlockImageManager>().ImageReload();
    }

    private bool CanShoot()
    {
        if (shootingBlock == null)
        {
            return true;
        }
        BlockAnimation blockAnimation = shootingBlock.GetComponent<BlockAnimation>();
        return blockAnimation.IsArrived && blockAnimation.IsMerged;
    }
}



