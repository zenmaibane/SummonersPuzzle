using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * <summary>
 * 現在のHPを受け取って、テキストとゲージに反映させる
 * </summary>
*/

public class HPChanger : MonoBehaviour {

	private int nowHP;
	private int maxHP;

	private Text HPText;
	private Image HPGauge;

	void Start () {
		HPText = transform.Find("Text").GetComponent<Text>();
		HPGauge = transform.Find("Gauge").GetComponent<Image>();
	}
	
	void Update () {
		float diff = HPGauge.fillAmount - GetPercentage();
		if (Mathf.Abs(diff) >= 0.01f)
		{
			HPGauge.fillAmount -= diff / 50;
		}
	}

	public void SetNowHP(int HP)
	{
		this.nowHP = HP;

		HPText.text = GenerateHPformat();
	}

	public void SetMaxHP(int maxHP)
	{
		this.maxHP = maxHP;
		SetNowHP(maxHP);
	}

	private float GetPercentage()
	{
		return (float)nowHP / (float)maxHP;
	}

	private string GenerateHPformat()
	{
		return nowHP + " / " + maxHP;
	}
}
