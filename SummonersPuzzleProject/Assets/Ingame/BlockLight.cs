﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * 各ブロックにアタッチさせ、輝かす処理を入れる
 * この光は、合体時に大きく広がる
 * </summary>
*/

public class BlockLight : MonoBehaviour {

	[SerializeField] private bool merge;
	private float alphaSpeed = 2f;

	void Start () {
		merge = false;
	}
	
	void Update () {
		if (merge)
		{
			//transform.localScale *= (alphaSpeed * Time.deltaTime) / 10000.0f;
			float nextScale = transform.localScale.x + (alphaSpeed * Time.deltaTime);
			transform.localScale = new Vector3(nextScale, nextScale);
			//print(transform.localScale + "\t" + (alphaSpeed * Time.deltaTime));
			Color color = GetComponent<SpriteRenderer>().color;
			color = new Color(color.r, color.g, color.b, color.a - alphaSpeed * Time.deltaTime);
			GetComponent<SpriteRenderer>().color = color;
			if (transform.localScale.x > 2)
			{
				Destroy(transform.parent.gameObject);
				//Destroy(this.gameObject);

				// デバッグ用
				/*
				transform.localScale = new Vector3(1,1);
				GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
				merge = false;
				*/
			}
		}
		else
		{

			float scale = Mathf.Sin(360 * Time.time * Mathf.Deg2Rad) / 10;
			scale += 0.75f;
			transform.localScale = new Vector3(scale, scale);
		}
	}

	public void Merge()
	{
		GetComponent<SpriteRenderer>().enabled = true;
		merge = true;
	}

	public void SetTarget()
	{
		GetComponent<SpriteRenderer>().enabled = true;
	}
}
