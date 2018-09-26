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

    private HPChanger myHPChanger;
    private HPChanger rivalHPChanger;
    private ForceManager forceManager;

    private int delayCounter; // 被ダメージメソッドがPhoton側から連続で2回実行されてしまったので、それを防ぐカウンター
    private int delayCounterFinish;  // 勝負がついた後すぐにシーン遷移すると、片方がゲーム画面に残されてしまうので、それを防ぐカウンター

    void Start()
    {
        myMaxHP = 2500;
        rivalMaxHP = 2500;
        myHP = myMaxHP;
        rivalHP = rivalMaxHP;

        myHPChanger = GameObject.Find("Canvas/MyHP").GetComponent<HPChanger>();
        rivalHPChanger = GameObject.Find("Canvas/RivalHP").GetComponent<HPChanger>();

        myHPChanger.SetMaxHP(myMaxHP);
        rivalHPChanger.SetMaxHP(rivalMaxHP);

        forceManager = GameObject.Find("ForceManager").GetComponent<ForceManager>();

        delayCounterFinish = -1;
    }

    void Update()
    {
        if (delayCounter > 0) delayCounter--;

        if (IsBattleFinished() && delayCounterFinish == -1)
        {
            delayCounterFinish = 50;
        }
        if (delayCounterFinish > 0)
        {
            delayCounterFinish--;
            print("delayCounterFinish : " + delayCounterFinish);
        }
        else if (delayCounterFinish == 0)
        {
            CompleteBattle();
        }
    }

    public void DamageRival(int totalRank)
    {
        //TODO: 実際のダメージ計算式は考える必要がある(ブーストゲージ考慮含め)
        var force = Mathf.CeilToInt(forceManager.force);
        if (force == 0)
        {
            force = 1;
        }
        int resultDamage = totalRank * 10 * force;


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
        }
        catch
        {
            Debug.LogWarning("マッチングされていません。\nなお、スペースキーを押すと、被ダメージテストができます。");
        }
        // TODO: 体力ゲージへの反映(テキストは実装済み)
        rivalHPChanger.SetNowHP(rivalHP);
    }

    public void BeHurt(int damage)
    {
        if (delayCounter > 0)
        {
            return;   // Photon側から連続でこのメソッドを呼び出されることを防ぐ
        }
        print("攻撃を受けました。　my HP : " + myHP + " -> " + (myHP - damage));
        delayCounter = 100;
        myHP -= damage;

        //TODO: 体力ゲージへの反映、ゲームオーバー判定、被ダメージエフェクトの処理
        myHPChanger.SetNowHP(myHP);
    }

    private string GenerateHPformat(int nowHP, int maxHP)
    {
        return nowHP + " / " + maxHP;
    }

    private bool IsBattleFinished()
    {
        return myHP <= 0 || rivalHP <= 0;
    }

    private void CompleteBattle()
    {
        bool lose = myHP <= float.Epsilon;
        bool win = rivalHP <= float.Epsilon;

        if (win && lose)
        {
            GameStateManager.Instance.BattleResult = BattleResult.Draw;
            Debug.Log("引き分け");
        }
        else if (win)
        {
            GameStateManager.Instance.BattleResult = BattleResult.Win;
            Debug.Log("勝ち");
        }
        else
        {
            GameStateManager.Instance.BattleResult = BattleResult.Lose;
            Debug.Log("負け");
        }

        IngameSceneController ingameSceneController =
            GameObject.Find("IngameSceneController").GetComponent<IngameSceneController>();
        ingameSceneController.LoadResultScene();
    }
}
