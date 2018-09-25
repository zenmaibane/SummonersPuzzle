using System.Collections;
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
	[SerializeField] private float alphaSpeed = 0.02f;

	void Start () {
		merge = false;
	}
	
	void Update () {
		if (merge)
		{
			transform.localScale *= 1.05f;
			Color color = GetComponent<SpriteRenderer>().color;
			color = new Color(color.r, color.g, color.b, color.a - alphaSpeed);
			GetComponent<SpriteRenderer>().color = color;
			if (transform.localScale.x > 10)
			{
				Destroy(this.gameObject);

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
		merge = true;
	}

	public void SetTarget()
	{
		GetComponent<SpriteRenderer>().enabled = true;
	}
}
