using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChange : MonoBehaviour {

	public GameObject photonmanager;

	private void Start()
	{
	}


	// Update is called once per frame
	void Update () {
		if(photonmanager == null)
		{
			photonmanager = GameObject.Find("ShareData(Clone)");
		}
		else
		{
			//print("getHP = " + photonmanager.GetComponent<PhotonVariable>().getHP.ToString());
			GetComponent<Text>().text = photonmanager.GetComponent<PhotonVariable>().getHP.ToString();
		}
	}
}
