using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum CharaName
{
    Hagane,
    Nyantaso,
    Kanade
}

public class CharaDataGenerator : MonoBehaviour
{
    public CharaData GenerateCharaData(CharaName charaName)
    {
        switch (charaName)
        {
            case CharaName.Hagane:
                return new CharaData(CharaName.Hagane, new BlockColor[] { BlockColor.Yellow, BlockColor.Green },
                                     maxHP: 1000, summonSpeedSec: 16f, minSummonLevel: 3, maxSummonLevel: 5);
            case CharaName.Nyantaso:
                return new CharaData(CharaName.Nyantaso, new BlockColor[] { BlockColor.Yellow },
                                     maxHP: 1000, summonSpeedSec: 10f, minSummonLevel: 1, maxSummonLevel: 4);
            case CharaName.Kanade:
                return new CharaData(CharaName.Kanade, new BlockColor[] { BlockColor.Red, BlockColor.Yellow, BlockColor.Green },
                                     maxHP: 1000, summonSpeedSec: 20f, minSummonLevel: 4, maxSummonLevel: 7);
            default:
                throw new ArgumentException($"{Enum.GetName(typeof(CharaName), charaName)} Data is not defined.");
        }
    }
}



