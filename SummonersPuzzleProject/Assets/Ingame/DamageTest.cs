using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * 被ダメージテスト用のスクリプト
 * スペースキーを押すと、10ダメージを受ける。
 * </summary>
*/

public class DamageTest : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GetComponent<HPManager>().BeHurt(10);
		}
	}
}
