using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * <summary>
 * このスクリプトがアタッチされたゲームオブジェクトは、
 * beingSceneSetに含まれるシーンにいる間は削除されない
 * </summary>
*/

public class PhotonDelete : MonoBehaviour {

	private HashSet<string> beingSceneSet;

	void Start ()
	{
		DontDestroyOnLoad(gameObject);
		SceneManager.activeSceneChanged += OnActiveSceneChanged;

		beingSceneSet = new HashSet<string>();
		beingSceneSet.Add("Matching");
		//beingSceneSet.Add("GameTest");
		beingSceneSet.Add("Ingame");
	}
	
	void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
	{
		Debug.Log(prevScene.name + "->" + nextScene.name);
		if(beingSceneSet.Contains(nextScene.name) == false)
		{
			Destroy(this.gameObject);
		}
	}
}
