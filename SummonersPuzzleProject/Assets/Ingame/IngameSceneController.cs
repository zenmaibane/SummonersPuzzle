using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameSceneController : MonoBehaviour
{
    void Awake()
    {
        //デバッグ用．本来はいらない
        SelfSceneManager.Instantiate(GameObject.Find("SceneManager"));
        GameStateManager.Instantiate(GameObject.Find("GameStateManager"));
        if (GameStateManager.Instance.MyCharaData == null)
        {
            GameStateManager.Instance.MyCharaData = new CharaData(CharaName.Alice, new BlockColor[] { BlockColor.Red, BlockColor.Yellow, BlockColor.Green },
                                    maxHP: 1000, summonSpeedSec: 14f, minSummonLevel: 2, maxSummonLevel: 3);
        }
    }

    public void LoadResultScene()
    {
        SelfSceneManager.Instance.LoadResultScene();
    }
}
