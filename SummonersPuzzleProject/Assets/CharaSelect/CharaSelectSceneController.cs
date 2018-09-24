using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaSelectSceneController : MonoBehaviour
{

    [SerializeField] private Button backButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button charaButton;
    [SerializeField] private CharaDataGenerator charaDataGenerator;

    private GameObject characters;

    private int charaIndex;
    void Start()
    {
        backButton?.onClick.AddListener(OnClickBackButton);
        selectButton?.onClick.AddListener(OnClickSelectButton);
        charaButton?.onClick.AddListener(OnClickCharaButton);
        characters = GameObject.Find("Characters");

        //リリース時には消す
        SelfSceneManager.Instantiate(GameObject.Find("SceneManager"));
        GameStateManager.Instantiate(GameObject.Find("GameStateManager"));
    }

    void OnClickBackButton()
    {
        SelfSceneManager.Instance.LoadHomeScene();
    }

    void OnClickCharaButton()
    {
        charaIndex = (charaIndex + 1) % characters.transform.childCount;
        var character = characters.transform.GetChild(charaIndex);
        var UIController = character.gameObject.GetComponent<UIController>();
        ChangeChara(character.gameObject.activeSelf, UIController);
    }

    void OnClickSelectButton()
    {
        CharaName charaName = (CharaName)Enum.ToObject(typeof(CharaName), charaIndex);

        GameStateManager.Instance.SelfCharaData =
            charaDataGenerator.GenerateCharaData(charaName);

        SelfSceneManager.Instance.LoadBattleScene();
    }

    private void ChangeChara(bool activeSelf, UIController UIController)
    {
        for (var i = 0; i < characters.transform.childCount; i++)
        {
            characters.transform.GetChild(i).gameObject.GetComponent<UIController>().Hide();
        }
        UIController.Show();
    }
}
