using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : MonoBehaviour
{

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && transform.name.Equals("ShareData(Mine)"))
		{
			GetComponent<PhotonVariable>().HP *= 2;
		}
	}
}
