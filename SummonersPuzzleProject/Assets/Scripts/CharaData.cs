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
    public float SummonSpeedSec { get; set; }
    public int MaxSummonLevel { get; }
    public int MinSummonLevel { get; }

    private float InitSummonSpeed { get; }
    public CharaData(CharaName charaName, BlockColor[] summonBlockColors,
                    int maxHP, float summonSpeedSec, int maxSummonLevel, int minSummonLevel)
    {
        CharaName = charaName;
        SummonBlockColors = summonBlockColors;
        MaxHP = maxHP;
        SummonSpeedSec = summonSpeedSec;
        MaxSummonLevel = maxSummonLevel;
        MinSummonLevel = minSummonLevel;

        InitSummonSpeed = summonSpeedSec;
    }

    public void ResetSummonSpeed()
    {
        SummonSpeedSec = InitSummonSpeed;
    }

}
