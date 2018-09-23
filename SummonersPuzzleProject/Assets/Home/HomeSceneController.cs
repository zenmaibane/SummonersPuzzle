using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeSceneController : MonoBehaviour
{

    [SerializeField] private Button vsButton;
    [SerializeField] private Button soloButton;
    [SerializeField] private Button howToButton;

    void Start()
    {
        vsButton?.onClick.AddListener(OnClickVsButton);
        soloButton?.onClick.AddListener(OnClickSoloButton);
        howToButton?.onClick.AddListener(OnClickHowToButton);
        
        SelfSceneManager.Instantiate(GameObject.Find("SceneManager"));
        GameStateManager.Instantiate(GameObject.Find("GameStateManager"));
    }

    void OnClickVsButton(){
        SelfSceneManager.Instance.LoadVsScene();
    }

	void OnClickSoloButton(){
        SelfSceneManager.Instance.LoadSoloScene();
	}

	void OnClickHowToButton(){
        SelfSceneManager.Instance.LoadHowToScene();
    }

}
