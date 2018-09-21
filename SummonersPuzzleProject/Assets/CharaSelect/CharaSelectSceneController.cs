﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaSelectSceneController : MonoBehaviour
{

    [SerializeField] private Button backButton;
    [SerializeField] private Button selectButton;

    private SelfSceneManager sceneManager;


    void Start()
    {
        backButton?.onClick.AddListener(OnClickBackButton);
        selectButton?.onClick.AddListener(OnClickSelectButton);

        //リリース時には消す
        SelfSceneManager.Instantiate(GameObject.Find("SceneManager"));        
        
        sceneManager = SelfSceneManager.Instance;
    }

    void OnClickBackButton(){
        sceneManager.LoadHomeScene();
    }

	void OnClickSelectButton(){
        sceneManager.LoadBattleScene();
	}
}