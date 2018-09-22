using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChange : MonoBehaviour {

	public GameObject photonmanager;

	private void Start()
	{
		DontDestroyOnLoad(this.gameObject);
		DontDestroyOnLoad(transform.parent.gameObject);
	}


	// Update is called once per frame
	void Update () {
		if(photonmanager == null)
		{
			photonmanager = GameObject.Find("ShareData(Mine)");
		}
		else
		{
			//print("getHP = " + photonmanager.GetComponent<PhotonVariable>().getHP.ToString());
			GetComponent<Text>().text = photonmanager.GetComponent<PhotonVariable>().attackDamage.ToString();
		}
	}
}
