using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour {

	private int startWordApperSpeed = 20;
	private int startWordWaitTime   = 80;
	//private int startWordBigTime    = 20;

	private GameObject gameStartImage;

	private bool gameStartImageFlag = false;

	void Start () {
		gameStartImage = GameObject.Find("Canvas/BattleStart");

	}
	
	void Update ()
	{
		// TODO: キャラを出現させるアニメーション

		// TODO: ブロックを登場させるアニメーション


		// ゲームスタート文字を表示させる
		if (gameStartImage != null)
		{
			Image startImage = gameStartImage.GetComponent<Image>();
			if (gameStartImage.GetComponent<RectTransform>().localScale.x < 1.2f)
			{
				if (startImage.fillAmount < 1)
				{
					startImage.fillAmount += ((float)startWordApperSpeed / 1000.0f);
				}
				else
				{
					if (startWordWaitTime > 0)
					{
						startWordWaitTime--;
					}
					else
					{
						gameStartImage.GetComponent<RectTransform>().localScale *= 1.01f;
						startImage.color = new Color(startImage.color.r, startImage.color.g, startImage.color.b, startImage.color.a * 0.9f);
					}
				}
			}
			else
			{
				Destroy(gameStartImage);
				GameObject.Find("Header/Timer").GetComponent<Timer>().StartGame();
			}
		}
	}
}
