using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonVariable : Photon.MonoBehaviour
{
    private GameObject hpManager;
    private AttackManager attackManager;
    public int attackDamage;
    public int charaNumber;
    //public int getHP;

    void Start()
    {
        //attackDamage = Random.Range(0, 100);
        attackDamage = 0;
        attackManager = GameObject.Find("AttackManager").GetComponent<AttackManager>();
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(attackDamage);
            stream.SendNext(charaNumber);

        }
        else
        {
            attackDamage = (int)stream.ReceiveNext();
            charaNumber = (int)stream.ReceiveNext();

            if (attackManager == null)
            {
                attackManager = GameObject.Find("AttackManager").GetComponent<AttackManager>();
            }

            attackManager.Attacked(attackDamage);
        }
    }
}
