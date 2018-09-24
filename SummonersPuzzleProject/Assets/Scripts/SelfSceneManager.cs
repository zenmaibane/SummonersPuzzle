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
        // 最初のシーンからやらなくてもいいようなデバッグ用関数
        GenerateDebugInstance(go);

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
        GameStateManager.Instance.PlayMode = PlayMode.VS;

        UnityEngine.SceneManagement.SceneManager.LoadScene("CharaSelect");
    }

    public void LoadSoloScene()
    {
        GameStateManager.Instance.PlayMode = PlayMode.Solo;

        UnityEngine.SceneManagement.SceneManager.LoadScene("CharaSelect");
    }

    public void LoadHowToScene()
    {
        Debug.Log("遊び方シーンを読み込んだよ");
        Debug.Log("余裕があったらチュートリアル作るよ");
    }

    public void LoadBattleScene()
    {
        // マッチングシーンの方が正しいそう
        UnityEngine.SceneManagement.SceneManager.LoadScene("Ingame");
        Debug.Log("バトルシーンを読み込んだよ");
    }

    public void LoadResultScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
    }

    private static void GenerateDebugInstance(GameObject sceneManager)
    {
        if (sceneManager == null && instance == null)
        {
            Debug.Log("Debug用のインスタンスを作成しました");
            var prefab = (GameObject)Resources.Load("SceneManager");
            var gameobject = Instantiate(prefab, Vector2.zero, Quaternion.identity);
            instance = gameobject.GetComponent<SelfSceneManager>();
        }
    }
}
