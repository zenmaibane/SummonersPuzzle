using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum CharaName
{
    Alice,
    Becca,
    Charlotte
}

public class CharaDataGenerator : MonoBehaviour
{
    public CharaData GenerateCharaData(CharaName charaName)
    {
        switch (charaName)
        {
            case CharaName.Alice:
                return new CharaData(CharaName.Alice, new BlockColor[] { BlockColor.Red, BlockColor.Yellow, BlockColor.Green },
                                     maxHP:1000 ,summonSpeedSec: 14f, minSummonLevel: 2, maxSummonLevel: 3);
            case CharaName.Becca:
                return new CharaData(CharaName.Becca, new BlockColor[] { BlockColor.Yellow, BlockColor.Green },
                                     maxHP:1000 ,summonSpeedSec: 10f, minSummonLevel: 1, maxSummonLevel: 3);
            case CharaName.Charlotte:
                return new CharaData(CharaName.Charlotte, new BlockColor[] { BlockColor.Yellow },
                                     maxHP:1000 ,summonSpeedSec: 7f, minSummonLevel: 1, maxSummonLevel: 2);
            default:
                throw new ArgumentException($"{Enum.GetName(typeof(CharaName), charaName)} Data is not defined.");
        }
    }
}



