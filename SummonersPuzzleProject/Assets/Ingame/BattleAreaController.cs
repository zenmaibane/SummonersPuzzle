using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAreaController : MonoBehaviour
{
    private bool isGenerated = false;

    private void Update()
    {
        if (isGenerated == false)
        {
            CharaData myCharaData = GameStateManager.Instance.MyCharaData;

            CharaData rivalCharaData = null;

            Debug.Log($"PlayMode: {GameStateManager.Instance.PlayMode}");
            if (GameStateManager.Instance.PlayMode == PlayMode.Solo)
            {
                rivalCharaData = GetComponent<CharaDataGenerator>().GenerateCharaData(CharaName.Kakashi);
            }
            else
            {
                GameObject shareDataClone = GameObject.Find("ShareData(Clone)");
                if (shareDataClone == null)
                {
                    return;
                }
                int rivalCharaNumber = shareDataClone.GetComponent<PhotonVariable>().charaNumber;
                rivalCharaData = GetComponent<CharaDataGenerator>().GenerateCharaData((CharaName)rivalCharaNumber);
				var rivalHPChanger = GameObject.Find("Canvas/RivalHP").GetComponent<HPChanger>();
        		rivalHPChanger.SetMaxHP(rivalCharaData.MaxHP);
            }

            var myChara = GameObject.Find("MyChara");
            GenerateBattleChara(myChara, myCharaData, false);
            var rivalChara = GameObject.Find("RivalChara");
            GenerateBattleChara(rivalChara, rivalCharaData, true);
            isGenerated = true;
        }
    }

    private void GenerateBattleChara(GameObject chara, CharaData charaData, bool isRivalChara)
    {
        string charaName = Enum.GetName(typeof(CharaName), charaData.CharaName);
        var charaPrefab = Resources.Load<GameObject>($"Chara/{charaName}");
        var go = Instantiate(charaPrefab, chara.transform.position, Quaternion.identity);
        var image = go.transform.GetChild(0);
        if (isRivalChara)
        {
            image.GetComponent<SpriteRenderer>().flipX = false;
        }
        go.transform.parent = chara.transform;
        image.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
}
