﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainLoadScene : MonoBehaviour {

    [Header("次のシーンに遷移するとき")]
    public GameObject fadeIN;

    //public Button startButton;

    bool filst = true;

    string sceneName;

    // Use this for initialization
    void Start () {
        //startButton.Select();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnRetryButton()
    {
        sceneName = "Main";

        if (filst)
        {
            filst = false;
            AudioManager.Instance.PlaySE("Button");
            StartCoroutine(WaitFadeIn(sceneName));
        }

        //PlayerMoveSetting.filst = true;
    }

    public void OnStageSelectButton()
    {
        sceneName = "StageSelect";

        if (filst)
        {
            filst = false;
            AudioManager.Instance.PlaySE("Button");
            StartCoroutine(WaitFadeIn(sceneName));
        }

        PlayerMoveSetting.playerCount = 0;
    }

    public void OnTitleButton()
    {
        sceneName = "Title";

        if (filst)
        {
            filst = false;
            AudioManager.Instance.PlaySE("Button");
            StartCoroutine(WaitFadeIn(sceneName));
        }

        PlayerMoveSetting.playerCount = 0;
    }

    /// <summary>
    /// フェードするときの調整用
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitFadeIn(string scene)
    {
        fadeIN.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(scene);
        PlayerMoveSetting.Instance.MoveSettingFlg = true;
    }
}
