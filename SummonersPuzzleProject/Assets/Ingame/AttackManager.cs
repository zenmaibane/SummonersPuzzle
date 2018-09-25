using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackManager : MonoBehaviour
{

    private GameObject myChara;
    private GameObject rivalChara;

    private GameObject monster1Prefab;

    private int delayCounter; // 被ダメージメソッドがPhoton側から連続で2回実行されてしまったので、それを防ぐカウンター


    void Start()
    {
        monster1Prefab = (GameObject)Resources.Load("Monster1");
        myChara = GameObject.Find("MyChara");
        rivalChara = GameObject.Find("RivalChara");
    }

    void Update()
    {
        if (delayCounter > 0) delayCounter--;
    }

    public void Attack(int totalRank)
    {
        if (totalRank > 0)
        {
            Debug.Log("Attack");
            var go = Instantiate(monster1Prefab, myChara.transform.position, Quaternion.identity);
            go.transform.parent = myChara.transform;
            go.GetComponent<AttackAnimation>().SummonTotalRank = totalRank;
            go.GetComponent<AttackAnimation>().MyAttack = true;
        }
    }
    public void Attacked(int damage)
    {
        if (delayCounter > 0)
        {
            return;   // Photon側から連続でこのメソッドを呼び出されることを防ぐ
        }
        delayCounter = 100;
        if (damage > 0)
        {
            Debug.Log($"Attacked {damage}");
            var go = Instantiate(monster1Prefab, rivalChara.transform.position, Quaternion.identity);
            go.transform.parent = rivalChara.transform;
            go.GetComponent<AttackAnimation>().Damage = damage;
            go.GetComponent<AttackAnimation>().MyAttack = false;
        }
    }

}
