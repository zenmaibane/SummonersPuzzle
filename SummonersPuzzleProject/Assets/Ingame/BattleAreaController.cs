using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAreaController : MonoBehaviour
{

    private CharaData myCharaData;

    private CharaData rivalCharaData;

    void Start()
    {
        // myCharaData = GameStateManager.Instance.MyCharaData;
        // rivalCharaData = <いい感じに処理しましょう:pray:>

        myCharaData = new CharaData(CharaName.Alice, new BlockColor[] { BlockColor.Red, BlockColor.Yellow, BlockColor.Green },
                                     maxHP: 1000, summonSpeedSec: 14f, minSummonLevel: 2, maxSummonLevel: 3);

        var myChara = GameObject.Find("MyChara");
        string myCharaName = Enum.GetName(typeof(CharaName), myCharaData.CharaName);
        var charaPrefab = Resources.Load<GameObject>($"Chara/{myCharaName}");
        var chara = Instantiate(charaPrefab, myChara.transform.position, Quaternion.identity);
        chara.transform.parent = myChara.transform;
    }

    void Update()
    {

    }
}
