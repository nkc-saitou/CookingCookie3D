using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultSceneBGM : MonoBehaviour {

    [Header("次のシーンに遷移するとき")]
    public GameObject fadeIN;

    [Header("違うシーンから遷移してきたとき")]
    public GameObject fadeOut;

    public Button selectButton;

    string sceneName;

    bool filst = true;

    public Text scoreText;
    public Text highText;

    // Use this for initialization
    void Start () {
        fadeOut.SetActive(false);
        fadeOut.SetActive(true);

        selectButton.Select();
        StartCoroutine(ScoreManagerS.Instance.ScoreAnimation(0, ScoreManagerS.Instance.Score, 5.0f, scoreText));

        highText.text = ScoreManagerS.Instance.HighScore.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TitleButton()
    {
        sceneName = "Title";

        StartCoroutine(WaitFadeIn());
    }

    public void StageSelectButton()
    {
        sceneName = "StageSelect";

        StartCoroutine(WaitFadeIn());
    }

    /// <summary>
    /// フェードするときの調整用
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitFadeIn()
    {
        if (filst == true)
        {
            filst = false;
            AudioManager.Instance.PlaySE("Button");
        }

        fadeIN.SetActive(true);
        ScoreManagerS.Instance.Reset();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(sceneName);
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

}
