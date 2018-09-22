using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonVariable : Photon.MonoBehaviour
{
	private GameObject hpManager;
	public int attackDamage;
	//public int getHP;

	void Start () {
		//attackDamage = Random.Range(0, 100);
		attackDamage = 0;
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(attackDamage);
			//stream.SendNext(_fuga);

		}
		else
		{
			attackDamage = (int)stream.ReceiveNext();
			//_fuga = (int)stream.ReceiveNext();

			if(hpManager == null)
			{
				hpManager = GameObject.Find("HPManager");
			}
			hpManager.GetComponent<HPManager>().BeHurt(attackDamage);
		}
	}
}
