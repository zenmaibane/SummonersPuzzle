using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelfSceneManager : MonoBehaviour
{

    private static SelfSceneManager instance;
    public static SelfSceneManager Instance
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
        instance = go.GetComponent<SelfSceneManager>() ?? go.AddComponent<SelfSceneManager>();
        if (UnityEngine.Application.isPlaying)
        {
            DontDestroyOnLoad(instance.gameObject);
        }
    }

    public void LoadHomeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
    }

    public void LoadVsScene()
    {
        //2人プレイであることの設定の記述が必要

        UnityEngine.SceneManagement.SceneManager.LoadScene("CharaSelect");
    }

    public void LoadSoloScene()
    {
        //1人プレイであることの設定の記述が必要

        UnityEngine.SceneManagement.SceneManager.LoadScene("CharaSelect");
    }

    public void LoadHowToScene()
    {
        Debug.Log("遊び方シーンを読み込んだよ");
        Debug.Log("余裕があったらチュートリアル作るよ");
    }

    public void LoadBattleScene()
    {
        Debug.Log("バトルシーンを読み込んだよ");
    }
}
