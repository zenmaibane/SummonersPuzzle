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

    bool finishedSummonAnimation = false;
    float moveSpeed = 2.0f;

    void Start()
    {
        myChara = GameObject.Find("MyChara");
        rivalChara = GameObject.Find("RivalChara");
    }

    void Update()
    {
        if (MyAttack)
        {
            this.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            if (this.transform.position.x - rivalChara.transform.position.x >= float.Epsilon)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            this.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            if (this.transform.position.x - myChara.transform.position.x <= float.Epsilon)
            {
                Destroy(this.gameObject);
            }
        }
        this.transform.position += new Vector3(0, Mathf.Sin(360 * Time.time * Mathf.Deg2Rad) / 30, 0);
    }
}
