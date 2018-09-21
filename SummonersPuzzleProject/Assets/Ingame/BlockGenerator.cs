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

    // Use this for initialization
    void Start()
    {
        nextBlocks = new Queue<BlockData>();
        nextBlocks.Enqueue(GenerateBlockData());
        nextBlocks.Enqueue(GenerateBlockData());
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
        int rank = (int)Math.Round(UnityEngine.Random.Range(1.0f, 5.0f));

        // TODO: キャラが使える色に応じたものを取得する(現状は3色からランダム)
        BlockColor[] blockColors = { BlockColor.Red, BlockColor.Yellow, BlockColor.Green };

        int randomColor = (int)Math.Round(UnityEngine.Random.Range(0.0f, blockColors.Length-1));
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
