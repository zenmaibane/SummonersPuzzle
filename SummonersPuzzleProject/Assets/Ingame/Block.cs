using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * ブロックの基本的な情報を保持する。（アタッチ用）
 * 　例：ランク、色など
 * </summary>
*/
public class Block : MonoBehaviour
{

    public BlockData blockData { get; private set; }

    // Use this for initialization
    void Awake()
    {
        var blockGenerator = GameObject.Find("BlockStarter").GetComponent<BlockGenerator>();
        blockData = blockGenerator.GetNextBlock();
        Debug.Log(blockData.Color);
        Debug.Log(blockData.Rank);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
