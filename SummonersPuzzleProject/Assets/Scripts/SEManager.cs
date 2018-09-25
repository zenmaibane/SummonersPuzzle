using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour {

	private AudioSource audioSource;

	static public SEManager instance;

	[SerializeField] private AudioClip decision;
	[SerializeField] private AudioClip back;
	[SerializeField] private AudioClip charaChoice;
	[SerializeField] private AudioClip intoIngame;
	[SerializeField] private AudioClip shoot;
	[SerializeField] private AudioClip collide;
	[SerializeField] private AudioClip merge;
	[SerializeField] private AudioClip summon;
	[SerializeField] private AudioClip attack;
	[SerializeField] private AudioClip damage;
	[SerializeField] private AudioClip win;
	[SerializeField] private AudioClip lose;
	[SerializeField] private AudioClip draw;
	
	void Start () {
		audioSource = GetComponent<AudioSource>();

		//PlaySE("decision");
	}

	public void PlaySE(string name)
	{
		PlaySE(name, 1);
	}

	public void PlaySE(string name, float volumeScale)
	{
		var clip = GetType().GetField(name, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

		audioSource.PlayOneShot((AudioClip)clip.GetValue(this), volumeScale);
	}
}
