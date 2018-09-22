using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * <summary>
 * カウントダウンが０になったことを受け取って、相手に与えるダメージを計算する。
 * その後、1列目を召喚（削除）する。
 * </summary>
*/

public class HPManager : MonoBehaviour
{
    private GameObject photonObject;
	private int myMaxHP;
	private int rivalMaxHP;
	[SerializeField] private int myHP;
	[SerializeField] private int rivalHP;

	private Text myHPText;
	private Text rivalHPText;

	private int delayCounter; // 被ダメージメソッドがPhoton側から連続で2回実行されてしまったので、それを防ぐカウンター

	void Start()
    {
		myMaxHP = 300;     // TODO: 選択されているキャラ情報から初期HPを取得する必要あり
		rivalMaxHP = 300;  // TODO: 敵のから初期HPを取得する必要あり
		myHP = myMaxHP;
		rivalHP = rivalMaxHP;

		myHPText = GameObject.Find("Canvas/TextMyHP").GetComponent<Text>();
		rivalHPText = GameObject.Find("Canvas/TextRivalHP").GetComponent<Text>();

		print("myHPText : " + myHPText);
		myHPText.text = GenerateHPformat(myHP, myMaxHP);
		rivalHPText.text = GenerateHPformat(rivalHP, rivalMaxHP);
	}

	void Update()
	{
		if (delayCounter > 0) delayCounter--;
	}

	public void DamageRival(int totalRank)
    {
        //TODO: 実際のダメージ計算式は考える必要がある(ブーストゲージ考慮含め)
        int resultDamage = totalRank * 10;


		// ライバルにダメージを与える処理
		print("相手にダメージを与えました。　ダメージ量：" + resultDamage);
		rivalHP -= resultDamage;
		if (photonObject == null)
		{
			photonObject = GameObject.Find("ShareData(Mine)");
		}
		try
		{
			photonObject.GetComponent<PhotonVariable>().attackDamage = resultDamage;
		}catch{
			Debug.LogWarning("マッチングされていません。\nなお、スペースキーを押すと、被ダメージテストができます。");
		}
		// TODO: 体力ゲージへの反映(テキストは実装済み)
		rivalHPText.text = GenerateHPformat(rivalHP, rivalMaxHP);
	}

	public void BeHurt(int damage)
	{
		if(delayCounter > 0)
		{
			return;   // Photon側から連続でこのメソッドを呼び出されることを防ぐ
		}
		print("攻撃を受けました。　my HP : " + myHP + " -> " + (myHP - damage));
		delayCounter = 100;
		myHP -= damage;

		//TODO: 体力ゲージへの反映、ゲームオーバー判定、被ダメージエフェクトの処理
		myHPText.text = GenerateHPformat(myHP, myMaxHP);

	}

	private string GenerateHPformat(int nowHP, int maxHP)
	{
		return nowHP + " / " + maxHP;
	}
}
