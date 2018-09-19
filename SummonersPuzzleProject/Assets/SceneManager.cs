using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{

    private static SceneManager instance;
    public static SceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                throw new Exception("There is no SavingManager instance in scene");
            }
            return instance;
        }
    }
    public static void Instantiate(GameObject go)
    {
        instance = go.GetComponent<SceneManager>() ?? go.AddComponent<SceneManager>();
        if (UnityEngine.Application.isPlaying)
        {
            DontDestroyOnLoad(instance.gameObject);
        }
    }

    public void LoadVsScene() {
        //2人プレイであることの設定の記述が必要

        UnityEngine.SceneManagement.SceneManager.LoadScene("CharaSelect");
    }

    public void LoadSoloScene() { 
        //1人プレイであることの設定の記述が必要

        UnityEngine.SceneManagement.SceneManager.LoadScene("CharaSelect");        
    }

    public void LoadHowToScene() {
        Debug.Log("遊び方シーンを読み込んだよ");
        Debug.Log("余裕があったらチュートリアル作るよ");
    }

}
