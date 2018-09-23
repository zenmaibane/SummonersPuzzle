using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaData
{
    public CharaName CharaName { get; }
    public BlockColor[] SummonBlockColors { get; private set; }
    public int MaxHP { get; }
    public float SummonSpeed { get; set; }

    public int MaxSummonLevel { get; }
    public int MinSummonLevel { get; }



    public CharaData(CharaName charaName, BlockColor[] summonBlockColors,
                    int maxHP ,float summonSpeed, int maxSummonLevel, int minSummonLevel)
    {
        CharaName = charaName;
        SummonBlockColors = summonBlockColors;
        MaxHP = maxHP;
        SummonSpeed = summonSpeed;
        MaxSummonLevel = maxSummonLevel;
        MinSummonLevel = minSummonLevel;
    }

}
