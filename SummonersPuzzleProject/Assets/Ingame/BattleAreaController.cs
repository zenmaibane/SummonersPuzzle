using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAreaController : MonoBehaviour
{
	private bool isGenerated = false;

	private void Update()
	{
		if(isGenerated == false)
		{
			GameObject shareDataClone = GameObject.Find("ShareData(Clone)");
			if(shareDataClone == null){
				return;
			}
			CharaData myCharaData = GameStateManager.Instance.MyCharaData;
			int rivalCharaNumber = shareDataClone.GetComponent<PhotonVariable>().charaNumber;
			var rivalCharaData = GetComponent<CharaDataGenerator>().GenerateCharaData((CharaName)rivalCharaNumber);

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

	private IEnumerator Setup()
	{
		// リリース
		CharaData myCharaData = GameStateManager.Instance.MyCharaData;
		GameObject shareDataClone = GameObject.Find("ShareData(Clone)");
		while (shareDataClone == null){
			shareDataClone = GameObject.Find("ShareData(Clone)");
		}
		int rivalCharaNumber = shareDataClone.GetComponent<PhotonVariable>().charaNumber;
		CharaDataGenerator generator = new CharaDataGenerator();
		CharaData rivalCharaData = generator.GenerateCharaData((CharaName)rivalCharaNumber);

		// デバッグ
		//CharaData rivalCharaData = new CharaData(CharaName.Hagane, new BlockColor[] { BlockColor.Red, BlockColor.Yellow, BlockColor.Green },
		//                            maxHP: 1000, summonSpeedSec: 14f, minSummonLevel: 2, maxSummonLevel: 3);

		var myChara = GameObject.Find("MyChara");
		GenerateBattleChara(myChara, myCharaData, false);
		var rivalChara = GameObject.Find("RivalChara");
		GenerateBattleChara(rivalChara, rivalCharaData, true);
		/*
		for (int i = 1; i <= 10; i++)
		{
			Debug.Log(i);
			yield return new WaitForSeconds(1.0f);
		}
		*/
		yield return 0;
	}
}
