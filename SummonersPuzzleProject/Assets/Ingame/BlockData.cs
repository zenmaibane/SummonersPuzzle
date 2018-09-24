using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * ブロックの基本的な情報を保持する。（アタッチしないよう）
 * 　例：ランク、色など
 * </summary>
*/
public class BlockData
{
    public int Rank { get; set; }
    public BlockColor Color { get; private set; }

    public BlockData(int rank, BlockColor color)
    {
        this.Rank = rank;
        this.Color = color;
    }

    public override string ToString()
    {
        return $"rank: {Rank}, Color:{Color}";
    }
}
