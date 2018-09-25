using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameSceneController : MonoBehaviour
{
    public void LoadResultScene()
    {
        SelfSceneManager.Instance.LoadResultScene();
    }
}
