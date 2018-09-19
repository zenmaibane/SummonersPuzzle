using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonVariable : Photon.MonoBehaviour
{

	public int HP;
	public int getHP;

	// Use this for initialization
	void Start () {
		HP = Random.Range(0, 100);
	}

	// Update is called once per frame
	void Update()
	{
		HP = Random.Range(0, 100);
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(HP);
			//stream.SendNext(_fuga);
		}
		else
		{
			getHP = (int)stream.ReceiveNext();
			//_fuga = (int)stream.ReceiveNext();
		}
	}
}
