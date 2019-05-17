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

    int score;
    int highScore;

    string sceneName;

    bool filst = true;

    //public Text scoreText;
    //public Text highText;

    public GameObject highScoreImg;

    // Use this for initialization
    void Start () {
        fadeOut.SetActive(false);
        fadeOut.SetActive(true);

        score = ScoreManagerS.Instance.Score;
        highScore = ScoreManagerS.Instance.HighScore;

        selectButton.Select();
        //StartCoroutine(ScoreManagerS.Instance.ScoreAnimation(0, score, 5.0f, scoreText));

        //highText.text = highScore.ToString();

        //if (ScoreManagerS.Instance.Score > ScoreManagerS.Instance.HighScore) highScoreImg.SetActive(true);     
        //else highScoreImg.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //ScoreManagerS.Instance.ScoreRenew();
        
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
