using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * カウントダウンが０になったことを受け取って、相手に与えるダメージを計算する。
 * その後、1列目を召喚（削除）する。
 * </summary>
*/

public class HPManager : MonoBehaviour
{
    private GameObject rivalChara;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamageRival(int totalRank)
    {
        //TODO: 実際のダメージ計算式は考える必要がある(ブーストゲージ考慮含め)
        int resultDamage = totalRank * 10;

		// ライバルにダメージを与える処理
    }
}
