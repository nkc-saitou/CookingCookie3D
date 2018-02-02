using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {

    [Header("次のシーンに遷移するとき")]
    public GameObject fadeIN;

    [Header("違うシーンから遷移してきたとき")]
    public GameObject fadeOut;

    void Start () {
        fadeOut.SetActive(true);
        StartCoroutine(WaitFadeOut());
        AudioManager.Instance.PlayBGM("Title");
	}
	

	void Update () {

        StartCoroutine(TitleWait());
	}

    /// <summary>
    /// フェードするときの調整用
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitFadeIn()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("StageSelect");
    }

    /// <summary>
    /// 遷移してきたときの調整用
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitFadeOut()
    {
        yield return new WaitForSeconds(2.5f);
        fadeOut.SetActive(false);
    }

    IEnumerator TitleWait()
    {
        yield return new WaitForSeconds(1.5f);

        if (Input.GetButtonDown("JoyStick_Action1"))
        {
            AudioManager.Instance.PlaySE("Button");
            fadeIN.SetActive(true);
            StartCoroutine(WaitFadeIn());
        }
    }
}
