using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BGMManager : MonoBehaviour {

	private AudioSource audioSource;
	[SerializeField] private AudioClip BGM;
	[SerializeField] private AudioClip ingameBGM;

	static public BGMManager instance;
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

	}

	void Start () {
		audioSource = GetComponent<AudioSource>();
		SceneManager.activeSceneChanged += OnActiveSceneChanged;

		audioSource.Play();
	}

	// シーン遷移時に呼び出される
	void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
	{
		Debug.Log(prevScene.name + "->" + nextScene.name);

		if (nextScene.name.Equals("Ingame"))
		{
			audioSource.clip = ingameBGM;
			audioSource.Play();
		}
		
		if (audioSource.clip == ingameBGM && nextScene.name.Equals("Ingame") == false)
		{
			audioSource.clip = BGM;
			audioSource.Play();
		}
	}
}
