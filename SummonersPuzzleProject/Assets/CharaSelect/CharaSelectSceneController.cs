using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaSelectSceneController : MonoBehaviour
{

    [SerializeField] private Button backButton;
    [SerializeField] private Button selectButton;

    void Start()
    {
        backButton?.onClick.AddListener(OnClickBackButton);
        selectButton?.onClick.AddListener(OnClickSelectButton);

        //リリース時には消す
        SelfSceneManager.Instantiate(GameObject.Find("SceneManager"));
        GameStateManager.Instantiate(GameObject.Find("GameManager"));          
    }

    void OnClickBackButton(){
        SelfSceneManager.Instance.LoadHomeScene();
    }

	void OnClickSelectButton(){
        SelfSceneManager.Instance.LoadBattleScene();
	}
}
