using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager instance;

    public BattleResult BattleResult { get; set; }
    public PlayMode PlayMode { get; set; }


    public static GameStateManager Instance
    {
        get
        {
            if (instance == null)
            {
                throw new Exception("There is no GameStateManager instance in scene");
            }
            return instance;
        }
    }
    public static void Instantiate(GameObject go)
    {
        // 最初のシーンからやらなくてもいいようなデバッグ用関数
        GenerateDebugInstance(go);

        instance = go.GetComponent<GameStateManager>() ?? go.AddComponent<GameStateManager>();
        if (UnityEngine.Application.isPlaying)
        {
            DontDestroyOnLoad(instance.gameObject);
        }
    }

    private static void GenerateDebugInstance(GameObject gameStateManager)
    {
        if (gameStateManager == null && instance == null)
        {
            Debug.Log("Debug用のインスタンスを作成しました");
            var prefab = (GameObject)Resources.Load("GameStateManager");
            var gameobject = Instantiate(prefab, Vector2.zero, Quaternion.identity);
            instance = gameobject.GetComponent<GameStateManager>();
            Debug.Log(instance);
        }
    }
}
