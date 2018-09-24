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

    void Start()
    {
        backButton?.onClick.AddListener(OnClickBackButton);
        selectButton?.onClick.AddListener(OnClickSelectButton);
        charaButton?.onClick.AddListener(OnClickCharaButton);

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
        var characters = GameObject.Find("Characters");
        var alice = characters.transform.Find("Alice");
        var UIController = alice.gameObject.GetComponent<UIController>();
        Debug.Log(alice);
        if (alice.gameObject.activeSelf)
        {
            UIController.Hide();
        }
        else
        {
            UIController.Show();
        }
    }

    void OnClickSelectButton()
    {
        GameStateManager.Instance.SelfCharaData =
            charaDataGenerator.GenerateCharaData(CharaName.Alice);

        SelfSceneManager.Instance.LoadBattleScene();
    }
}
