using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaData
{
    public CharaName CharaName { get; }
    public BlockColor[] SummonBlockColors { get; }
    public int MaxHP { get; }
    public float SummonSpeed { get; set; }
    public int MaxSummonLevel { get; }
    public int MinSummonLevel { get; }

    private float InitSummonSpeed { get; }
    public CharaData(CharaName charaName, BlockColor[] summonBlockColors,
                    int maxHP, float summonSpeed, int maxSummonLevel, int minSummonLevel)
    {
        CharaName = charaName;
        SummonBlockColors = summonBlockColors;
        MaxHP = maxHP;
        SummonSpeed = summonSpeed;
        MaxSummonLevel = maxSummonLevel;
        MinSummonLevel = minSummonLevel;

        InitSummonSpeed = summonSpeed;
    }

    public void ResetSummonSpeed()
    {
        SummonSpeed = InitSummonSpeed;
    }

}
