using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * <summary>
 * ForceGaugeに直接アタッチされ、表示の切り替えなどアニメーションを担う
 * </summary>
*/

public class ForceChanger : MonoBehaviour {

	private bool gaugeMoving;
	private float tempTime;
	[SerializeField] private float force;
	private float viewForce;  // ゲージ用に、1.0fで割った余りの値

	private Image gauge;
	private Image soul;

	private bool twice;

	// Use this for initialization
	void Start () {
		gaugeMoving = false;
		force = 0;
		viewForce = 0;
		gauge = transform.Find("Gauge").GetComponent<Image>();
		soul = transform.Find("SoulBase/Soul").GetComponent<Image>();
	}
	
	void Update () {
		// ゲージを動かす処理
		if(gaugeMoving)
		{
			if (Mathf.Abs(gauge.fillAmount - viewForce) > 0.01f)
			{
				gauge.fillAmount += (viewForce - gauge.fillAmount) / 50f;
			}
			else
			{
				gaugeMoving = false;
				tempTime = 0;
			}
		}
		else
		{
			// ゲージをふわふわさせる処理
			gauge.fillAmount = viewForce + Mathf.Sin(180 * tempTime * Mathf.Deg2Rad) / 130;
			tempTime += Time.deltaTime;
		}

		if(soul.fillAmount < 1.0f && force >= 1.0f)
		{
			soul.fillAmount += 0.05f;
		}
	}

	public void SetNowForce(float force)
	{
		this.force = force;

		// 1.0fを超えるかどうか。（＝ゲージリセットされる状態かどうか）
		if(this.viewForce > force % 1.0f)
		{
			soul.fillAmount = 0;
			// TODO: 倍率が書かれた画像を差し替える処理

		}
		this.viewForce = force % 1.0f;
		gaugeMoving = true;
	}
}
