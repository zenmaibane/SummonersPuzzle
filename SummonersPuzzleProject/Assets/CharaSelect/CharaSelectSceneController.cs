using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaSelectSceneController : MonoBehaviour
{

    [SerializeField] private Button backButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button haganeButton;
    [SerializeField] private Button nyantasoButton;
    [SerializeField] private Button kanadeButton;

    [SerializeField] private CharaDataGenerator charaDataGenerator;

    private GameObject characters;
    private GameObject charaSelectButtons;

    private CharaName selectedChara;

    void Start()
    {
        backButton?.onClick.AddListener(OnClickBackButton);

        selectButton?.onClick.AddListener(OnClickSelectButton);

        haganeButton?.onClick.AddListener(OnClickHaganeButton);
        nyantasoButton?.onClick.AddListener(OnClickNyantasoButton);
        kanadeButton?.onClick.AddListener(OnClickKanadeButton);

        characters = GameObject.Find("Characters");
        charaSelectButtons = GameObject.Find("CharaSelectButtons");
        SelectChara(CharaName.Hagane);

        //リリース時には消す
        SelfSceneManager.Instantiate(GameObject.Find("SceneManager"));
        GameStateManager.Instantiate(GameObject.Find("GameStateManager"));
    }

    void OnClickBackButton()
    {
        SelfSceneManager.Instance.LoadHomeScene();
		GameObject.Find("SoundManager").GetComponent<SEManager>().PlaySE("back", 0.5f);
	}

    void OnClickHaganeButton()
    {
        selectedChara = CharaName.Hagane;
        SelectChara(selectedChara);
    }

    void OnClickNyantasoButton()
    {
        selectedChara = CharaName.Nyantaso;
        SelectChara(selectedChara);
    }

    void OnClickKanadeButton()
    {
        selectedChara = CharaName.Kanade;
        SelectChara(selectedChara);
    }

    void SelectChara(CharaName charaName)
    {
		GameObject.Find("SoundManager").GetComponent<SEManager>().PlaySE("charaChoice", 0.5f);
		int index = (int)charaName;
        for (var i = 0; i < characters.transform.childCount; i++)
        {
            if (i == index)
            {
                characters.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                characters.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        for (var i = 0; i < charaSelectButtons.transform.childCount; i++)
        {
            if (i == index)
            {
                charaSelectButtons.transform.GetChild(i).gameObject
                    .GetComponent<ButtonSelectAnimation>().Selected = true;
            }
            else
            {
                charaSelectButtons.transform.GetChild(i).gameObject
                    .GetComponent<ButtonSelectAnimation>().Selected = false;
            }
        }
    }

    void OnClickSelectButton()
    {
        GameStateManager.Instance.MyCharaData =
            charaDataGenerator.GenerateCharaData(selectedChara);
        Debug.Log(selectedChara);
        SelfSceneManager.Instance.LoadBattleScene();
		GameObject.Find("SoundManager").GetComponent<SEManager>().PlaySE("intoIngame", 0.5f);
	}

}
