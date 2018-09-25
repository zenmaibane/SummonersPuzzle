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


    void Start()
    {
        monster1Prefab = (GameObject)Resources.Load("Monster1");
        myChara = GameObject.Find("MyChara");
        rivalChara = GameObject.Find("RivalChara");
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
