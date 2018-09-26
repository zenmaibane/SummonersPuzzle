using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager instance;

    public BattleResult BattleResult { get; set; }
    public PlayMode PlayMode { get; set; }
    public CharaData MyCharaData { get; set; }

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
        if (instance == null)
        {
            instance = go.GetComponent<GameStateManager>() ?? go.AddComponent<GameStateManager>();
        }
        
        if (UnityEngine.Application.isPlaying)
        {
            DontDestroyOnLoad(instance.gameObject);
        }
    }
}
