using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAreaController : MonoBehaviour
{
    void Start()
    {

        // リリース
        CharaData myCharaData = GameStateManager.Instance.MyCharaData;
        // CharaData rivalCharaData = 相手の情報を取得

        // デバッグ
        CharaData rivalCharaData = new CharaData(CharaName.Alice, new BlockColor[] { BlockColor.Red, BlockColor.Yellow, BlockColor.Green },
                                    maxHP: 1000, summonSpeedSec: 14f, minSummonLevel: 2, maxSummonLevel: 3);

        var myChara = GameObject.Find("MyChara");
        GenerateBattleChara(myChara, myCharaData, false);
        var rivalChara = GameObject.Find("RivalChara");
        GenerateBattleChara(rivalChara, rivalCharaData, true);
    }

    private void GenerateBattleChara(GameObject chara, CharaData charaData, bool isRivalChara)
    {
        string charaName = Enum.GetName(typeof(CharaName), charaData.CharaName);
        var charaPrefab = Resources.Load<GameObject>($"Chara/{charaName}");
        var go = Instantiate(charaPrefab, chara.transform.position, Quaternion.identity);
        if (isRivalChara)
        {
            var image = go.transform.GetChild(0);
            image.GetComponent<SpriteRenderer>().flipX = false;
        }
        go.transform.parent = chara.transform;
    }
}
