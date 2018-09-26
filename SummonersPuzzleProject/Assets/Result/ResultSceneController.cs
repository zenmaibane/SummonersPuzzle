using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneController : MonoBehaviour
{
    [SerializeField] private Button homeButton;

    void Start()
    {
        homeButton?.onClick.AddListener(OnClickHomeButton);

        var charaIndex = (int)GameStateManager.Instance.MyCharaData.CharaName;
        var characters = GameObject.Find("Characters");
        for (var i = 0; i < characters.transform.childCount; i++)
        {
            if (charaIndex == i)
            {
                characters.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                characters.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        BattleResult battleResult = GameStateManager.Instance.BattleResult;
        var result = GameObject.Find("Result");
        var backGround = GameObject.Find("BackGround");
        switch (battleResult)
        {
            case BattleResult.Win:
			case BattleResult.Draw:
                result.transform.Find("Win").gameObject.SetActive(true);
                result.transform.Find("Lose").gameObject.SetActive(false);
                backGround.transform.Find("Win").gameObject.SetActive(true);
                backGround.transform.Find("Lose").gameObject.SetActive(false);
				GameObject.Find("SoundManager").GetComponent<SEManager>().PlaySE("win", 0.5f);
				break;
            case BattleResult.Lose:
                result.transform.Find("Win").gameObject.SetActive(false);
                result.transform.Find("Lose").gameObject.SetActive(true);
                backGround.transform.Find("Win").gameObject.SetActive(false);
                backGround.transform.Find("Lose").gameObject.SetActive(true);
				GameObject.Find("SoundManager").GetComponent<SEManager>().PlaySE("lose", 0.5f);
				break;
            default:
                throw new ArgumentException($"{Enum.GetName(typeof(BattleResult), battleResult)} Data is not defined.");
        }
    }

    void OnClickHomeButton()
    {
        SelfSceneManager.Instance.LoadHomeScene();
    }
}
