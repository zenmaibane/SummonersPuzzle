using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonSelectAnimation : MonoBehaviour
{
    private Vector2 initPos;
    private Vector2 selectedPos;

    public bool Selected = false;

    void Start()
    {
        initPos = this.transform.position;
        selectedPos = initPos + new Vector2(30, 0);
    }

    void Update()
    {
        if (Selected)
        {
            this.transform.position = selectedPos;
        }
        else
        {
            this.transform.position = initPos;
        }
    }
}



