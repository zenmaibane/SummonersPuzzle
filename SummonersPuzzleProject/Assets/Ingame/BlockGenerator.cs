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
    public Queue<BlockData> NextBlocks { get; private set; }
    private Queue<int> debugRankList;
    private int debugCounter;

    public readonly int BlockMaxCount = 5;

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

        NextBlocks = new Queue<BlockData>();
        for (var i = 0; i < BlockMaxCount; i++)
        {
            NextBlocks.Enqueue(GenerateBlockData());
        }
    }

    public BlockData GetNextBlock()
    {
        var nextBlock = NextBlocks.Dequeue();
        NextBlocks.Enqueue(GenerateBlockData());
        return nextBlock;
    }

    private BlockData GenerateBlockData()
    {
        // リリース用
        // var charaData = GameStateManager.Instance.SelfCharaData;
        // int rank = (int)Math.Round((double)UnityEngine.Random.Range(
        //     charaData.MinSummonLevel, charaData.MaxSummonLevel
        //     ));
        // BlockColor[] blockColors = charaData.SummonBlockColors;
        // Debug.Log(GameStateManager.Instance.SelfCharaData.CharaName);

        // デバッグ用
        int rank = (debugCounter % 2) + 1;  // 1と2を交互に出す
        debugCounter++;
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
