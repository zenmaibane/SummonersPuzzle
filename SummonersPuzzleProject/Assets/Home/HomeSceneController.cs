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
		GameObject.Find("SoundManager").GetComponent<SEManager>().PlaySE("decision", 0.5f);
    }

	void OnClickSoloButton(){
        SelfSceneManager.Instance.LoadSoloScene();
		GameObject.Find("SoundManager").GetComponent<SEManager>().PlaySE("decision", 0.5f);
	}

	void OnClickHowToButton(){
        SelfSceneManager.Instance.LoadHowToScene();
		GameObject.Find("SoundManager").GetComponent<SEManager>().PlaySE("decision", 0.5f);
	}

}
