﻿using System.Collections;
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
    private int rank;
    public int Rank { get; private set; }

    private BlockColor color;

    public BlockColor Color { get; private set; }

    public BlockData(int rank, BlockColor color)
    {
        this.Rank = rank;
        this.Color = color;
    }
}
