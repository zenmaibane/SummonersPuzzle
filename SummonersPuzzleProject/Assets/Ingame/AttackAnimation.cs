using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackAnimation : MonoBehaviour
{

    private bool myAttack = true;
    public bool MyAttack
    {
        get
        {
            return myAttack;
        }
        set
        {
            var image = this.transform.Find("Image").GetComponent<SpriteRenderer>();
            if (value)
            {
                image.flipX = false;
            }
            else
            {
                image.flipX = true;
            }
            this.myAttack = value;
        }
    }
    GameObject myChara;
    GameObject rivalChara;

    float moveSpeed = 2.0f;

    public int SummonTotalRank { get; set; }
    public int Damage { get; set; }
    private HPManager HPManager;

    void Start()
    {
        myChara = GameObject.Find("MyChara");
        rivalChara = GameObject.Find("RivalChara");

        HPManager = GameObject.Find("HPManager").GetComponent<HPManager>();
    }

    void Update()
    {
        if (MyAttack)
        {
            this.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            if (this.transform.position.x - rivalChara.transform.position.x >= float.Epsilon)
            {
                Destroy(this.gameObject);
                HPManager.DamageRival(SummonTotalRank);
            }
        }
        else
        {
            this.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            if (this.transform.position.x - myChara.transform.position.x <= float.Epsilon)
            {
                Destroy(this.gameObject);
                HPManager.BeHurt(Damage);
            }
        }
        this.transform.position += new Vector3(0, Mathf.Sin(360 * Time.time * Mathf.Deg2Rad) / 30, 0);
    }
}
