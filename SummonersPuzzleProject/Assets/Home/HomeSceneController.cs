using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeSceneController : MonoBehaviour
{

    [SerializeField] private Button vsButton;
    [SerializeField] private Button soloButton;
    [SerializeField] private Button howToButton;

    private SceneManager sceneManager;


    void Start()
    {
        vsButton?.onClick.AddListener(OnClickVsButton);
        soloButton?.onClick.AddListener(OnClickSoloButton);
        howToButton?.onClick.AddListener(OnClickHowToButton);
        SceneManager.Instantiate(GameObject.Find("SceneManager"));
        sceneManager = SceneManager.Instance;
    }

    void OnClickVsButton(){
        sceneManager.LoadVsScene();
    }

	void OnClickSoloButton(){
        sceneManager.LoadSoloScene();
	}

	void OnClickHowToButton(){
        sceneManager.LoadHowToScene();
    }

}
