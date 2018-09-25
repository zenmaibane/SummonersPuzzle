using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonManager : Photon.PunBehaviour
{
	public string ObjectName = "ShareData";
	private GameObject shareDataObject;

	void Start()
	{
		// Photonネットワークの設定を行う
		PhotonNetwork.ConnectUsingSettings("0.1");
		PhotonNetwork.sendRate = 30;


	}

	// 「ロビー」に接続した際に呼ばれるコールバック
	public override void OnJoinedLobby()
	{
		Debug.Log("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom();
	}

	// いずれかの「ルーム」への接続に失敗した際のコールバック
	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("OnPhotonRandomJoinFailed");

		// ルームを作成（今回の実装では、失敗＝マスタークライアントなし、として「ルーム」を作成）
		PhotonNetwork.CreateRoom(null);
	}

	// Photonサーバに接続した際のコールバック
	public override void OnConnectedToPhoton()
	{
		Debug.Log("OnConnectedToPhoton");
	}

	// マスタークライアントに接続した際のコールバック
	public override void OnConnectedToMaster()
	{
		Debug.Log("OnConnectedToMaster");
		PhotonNetwork.JoinRandomRoom();
	}

	// いずれかの「ルーム」に接続した際のコールバック
	public override void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");

		// 「ルーム」に接続したら共有データを生成する
		shareDataObject = PhotonNetwork.Instantiate(ObjectName, Vector3.zero, Quaternion.identity, 0);
		PhotonNetwork.playerName = "player" + (PhotonNetwork.playerList.Length % 2);
		print("your name = " + PhotonNetwork.playerName);
		shareDataObject.name = "ShareData(Mine)";

		ToGameStart();
	}

	// リモートプレイヤーが入室した際のコールバック
	public override void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		ToGameStart();
	}

	// もし2人揃ったらゲーム画面へ遷移
	private void ToGameStart()
	{
		if(PhotonNetwork.playerList.Length == 2)
		{
			shareDataObject.GetComponent<PhotonVariable>().charaNumber = (int)GameStateManager.Instance.MyCharaData.CharaName;
			SceneManager.LoadScene("Ingame");
		}
	}

	// 現在の接続状況を表示（デバッグ目的）
	void OnGUI()
	{
		//GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
}