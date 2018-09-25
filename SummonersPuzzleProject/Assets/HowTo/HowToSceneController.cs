using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToSceneController : MonoBehaviour
{
    [SerializeField] private Button homeButton;

    void Start()
    {
        homeButton?.onClick.AddListener(OnClickHomeButton);

    }

    void OnClickHomeButton()
    {
        SelfSceneManager.Instance.LoadHomeScene();
    }
}
