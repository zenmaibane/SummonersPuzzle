using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * <summary>
 * ForceGaugeに直接アタッチされ、表示の切り替えなどアニメーションを担う
 * </summary>
*/

public class ForceChanger : MonoBehaviour
{

    private bool gaugeMoving;
    private float tempTime;
    [SerializeField] private float force;
    private float viewForce;  // ゲージ用に、1.0fで割った余りの値

    private Image gauge;
    private Image soul;
    private Image number;

    private bool twice;

    // Use this for initialization
    void Start()
    {
        gaugeMoving = false;
        force = 0;
        viewForce = 0;
        gauge = transform.Find("Gauge").GetComponent<Image>();
        soul = transform.Find("SoulBase/Soul").GetComponent<Image>();
        number = transform.Find("Number").GetComponent<Image>();

        number.enabled = false;
    }

    void Update()
    {
        // ゲージを動かす処理
        if (gaugeMoving)
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

        if (soul.fillAmount < 1.0f && force >= 1.0f)
        {
            soul.fillAmount += 0.05f;
        }

        if (number.gameObject.transform.localScale.x >= 0.9f)
        {
            number.gameObject.transform.localScale -= new Vector3(0.05f, 0.05f);
        }
    }

    public void SetNowForce(float force)
    {
        this.force = force;

        // 1.0fを超えるかどうか。（＝ゲージリセットされる状態かどうか）
        if (this.viewForce > force % 1.0f)
        {
            soul.fillAmount = 0;
            // 倍率が書かれた画像を差し替える処理

            SetForceNumber(Mathf.CeilToInt(force));
            GameObject.Find("SoundManager").GetComponent<SEManager>().PlaySE("forceGaugeMax", 1f);
        }
        this.viewForce = force % 1.0f;
        gaugeMoving = true;
    }

    // Resourcesから各倍率のスプライトを読み込んで、表示させる
    private void SetForceNumber(int num)
    {
        if (num == 1) num++;   // たまに1になる場合が見受けられたので、無理やり修正
        number.enabled = true;
        Sprite sprite = Resources.Load<Sprite>("ForceGauge/x" + num);
        //print("ForceGauge/x" + num);
        number.sprite = sprite;

        // TODO: 効果音

    }
}
