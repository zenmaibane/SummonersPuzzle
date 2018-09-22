using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * <summary>
 * キャラクターの情報を保持して、次に生成するブロックを決定し、
 * ブロックを投げたら、次のブロックを生成する。
 * </summary>
*/

public class BlockGenerator : MonoBehaviour
{
    static readonly System.Random random = new System.Random();
    public Queue<BlockData> nextBlocks { get; private set; }

    private Queue<int> debugRankList;
    private int debugCounter;

    public static readonly int BlockMaxCount = 4;

    // Use this for initialization
    void Awake()
    {
        Init();

        debugRankList = new Queue<int>();
        debugRankList.Enqueue(1);
        debugCounter = 0;
    }

    private void Init()
    {
        nextBlocks = new Queue<BlockData>();
        for (var i = 0; i < BlockMaxCount; i++)
        {
            nextBlocks.Enqueue(GenerateBlockData());
        }
    }

    public BlockData GetNextBlock()
    {
        var nextBlock = nextBlocks.Dequeue();
        nextBlocks.Enqueue(GenerateBlockData());
        return nextBlock;
    }

    private BlockData GenerateBlockData()
    {
        // TODO: キャラ性能によって生成するランクの範囲を変える
        //int rank = (int)Math.Round(UnityEngine.Random.Range(1.0f, 5.0f));
        //int rank = (int)Math.Round(UnityEngine.Random.Range(1.0f, 2.0f));
        int rank = (debugCounter % 2) + 1;  // 1と2を交互に出す
        debugCounter++;

        // TODO: キャラが使える色に応じたものを取得する(現状は3色からランダム)
        //BlockColor[] blockColors = { BlockColor.Red, BlockColor.Yellow, BlockColor.Green };
        BlockColor[] blockColors = { BlockColor.Red };

        int randomColor = (int)Math.Round(UnityEngine.Random.Range(0.0f, blockColors.Length - 1));
        BlockData blockData = new BlockData(rank, blockColors[randomColor]);

        return blockData;
    }
    /*
	private Queue<int> GenerateRankList()
	{
		
	}
	*/
    private BlockData DebugGenerateBlockData()
    {
        // TODO: キャラ性能によって生成するランクの範囲を変える
        //int rank = (int)Math.Round(UnityEngine.Random.Range(1.0f, 5.0f));
        int rank = (int)Math.Round(UnityEngine.Random.Range(1.0f, 2.0f));

        // TODO: キャラが使える色に応じたものを取得する(現状は3色からランダム)
        //BlockColor[] blockColors = { BlockColor.Red, BlockColor.Yellow, BlockColor.Green };
        BlockColor[] blockColors = { BlockColor.Red };

        int randomColor = (int)Math.Round(UnityEngine.Random.Range(0.0f, blockColors.Length - 1));
        BlockData blockData = new BlockData(rank, blockColors[randomColor]);

        return blockData;
    }

    private static BlockColor RandomEnumValue()
    {
        return Enum
            .GetValues(typeof(BlockColor))
            .Cast<BlockColor>()
            .OrderBy(x => random.Next())
            .FirstOrDefault();
    }
}
