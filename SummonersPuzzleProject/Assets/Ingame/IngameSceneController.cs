using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameSceneController : MonoBehaviour
{
    void Start()
    {
        //デバッグ用．本来はいらない
        SelfSceneManager.Instantiate(GameObject.Find("SceneManager"));
    }

    public void LoadResultScene()
    { 
        SelfSceneManager.Instance.LoadResultScene();
    }
}
