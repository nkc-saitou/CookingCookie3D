using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLoadScene : MonoBehaviour {

    [Header("次のシーンに遷移するとき")]
    public GameObject fadeIN;

    string sceneName;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnRetryButton()
    {
        sceneName = "Main";
        StartCoroutine(WaitFadeIn(sceneName));
        PlayerMoveSetting.Instance.filst = true;
    }

    public void OnStageSelectButton()
    {
        sceneName = "StageSelect";
        StartCoroutine(WaitFadeIn(sceneName));
        PlayerMoveSetting.Instance.playerCount = 0;
    }

    public void OnTitleButton()
    {
        sceneName = "Title";
        StartCoroutine(WaitFadeIn(sceneName));
        PlayerMoveSetting.Instance.playerCount = 0;
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
