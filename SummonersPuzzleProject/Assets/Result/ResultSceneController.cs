using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneController : MonoBehaviour
{


    [SerializeField] private Button replayButton;
    [SerializeField] private Button charaSelectButton;
    [SerializeField] private Button homeButton;

    private SceneManager sceneManager;


    void Start()
    {
        replayButton?.onClick.AddListener(OnClickReplayButton);
        charaSelectButton?.onClick.AddListener(OnClickCharaSelectButton);
        homeButton?.onClick.AddListener(OnClickHomeButton);
        
		//本番時には消す
		SceneManager.Instantiate(GameObject.Find("SceneManager"));
        
		sceneManager = SceneManager.Instance;
    }

    void OnClickReplayButton()
    {
        sceneManager.LoadBattleScene();
    }

    void OnClickCharaSelectButton()
    {
        // その時プレイしているモードによる
        // sceneManager.LoadVsScene();
        sceneManager.LoadSoloScene();
    }

    void OnClickHomeButton()
    {
        sceneManager.LoadHomeScene();
    }
}
