using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonVariable : Photon.MonoBehaviour
{
	private GameObject hpManager;
	public int attackDamage;
	public string charaName;
	//public int getHP;

	void Start()
	{
		//attackDamage = Random.Range(0, 100);
		attackDamage = 0;
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(attackDamage);
			stream.SendNext(charaName);

		}
		else
		{
			attackDamage = (int)stream.ReceiveNext();
			charaName = (string)stream.ReceiveNext();

			if (hpManager == null)
			{
				hpManager = GameObject.Find("HPManager");
			}
			hpManager.GetComponent<HPManager>().BeHurt(attackDamage);
		}
	}
}
