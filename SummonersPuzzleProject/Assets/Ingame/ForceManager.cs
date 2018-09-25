using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * フォースゲージの値を管理する
 * なお、フォースの値は0～1.0fで一旦ゲージがMAXになる
 * </summary>
*/

public class ForceManager : MonoBehaviour {

	private float force;
	private ForceChanger forceChanger;

	void Start () {
		force = 0;
		forceChanger = GameObject.Find("Canvas/ForceGauge").GetComponent<ForceChanger>();
	}

	public void AddForce(float force)
	{
		this.force += force;
		forceChanger.SetNowForce(this.force);
	}
}
