using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyDeath : MonoBehaviour {

    [Header("エネミーを倒す数")]
    public int enemyDeath;

    [Header("エネミーの数表示用テキスト")]
    public Text deathText;

    public Button gameOver;


    int maxScore = 10000;

    //public Text ScoreText;

    //public GameObject resultCam;
    public GameObject UIcanvas;
    public GameObject gameOverImg;
    public GameObject gameOverUIImg;

    //最初の一度のみ
    bool first = false;

    public GameObject endImage;

    /// <summary>
    /// エネミーを倒した数
    /// </summary>
    public static int _EnemyDeath;

    [Header("次のシーンに遷移するとき")]
    public GameObject fadeIN;

    [Header("違うシーンから遷移してきたとき")]
    public GameObject fadeOut;


    [Header("北、南、西、東")]
    public WallHP[] wallHP;

    // Use this for initialization
    void Start ()
    {
        _EnemyDeath = enemyDeath;
        //fadeOut.SetActive(false);
        endImage.SetActive(false);
        //fadeIN.SetActive(false);
        //resultCam.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        deathText.text = _EnemyDeath.ToString();
    }

    void FixedUpdate()
    {
        if (_EnemyDeath == 0 && first == false)
        {
            AudioManager.Instance.PlaySE("Final");
            first = true;
            for (int i = 0; i < wallHP.Length; i++)
            {
                ScoreManagerS.Instance.AddScore(Mathf.FloorToInt(wallHP[i]._ScoreHP * maxScore));
                StartCoroutine(waitTime());
            }
            endImage.SetActive(true);

            //ScoreText.text = ScoreManagerS.Instance.Score.ToString();
        }

        if(WallBreak() && first == false)
        {
            first = true;
            gameOver.Select();
            AudioManager.Instance.PlaySE("Final");

            PlayerMoveSetting.Instance.MoveSettingFlg = false;

            gameOverImg.SetActive(true);
            gameOverUIImg.SetActive(true);
        }
    }

    bool WallBreak()
    {
        if(wallHP[0]._HP <= 0 && wallHP[1]._HP <= 0 && wallHP[2]._HP <= 0 && wallHP[3]._HP <= 0)
        {
            return true;
        }
        return false;
    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(2.0f);
        PlayerMoveSetting.Instance.MoveSettingFlg = false;
        fadeIN.SetActive(true);

        yield return new WaitForSeconds(2.0f);
        fadeIN.SetActive(false);
        fadeOut.SetActive(true);
        UIcanvas.SetActive(false);

        SceneManager.LoadScene("ResultScene");

        //yield return new WaitForSeconds(0.8f);
        //resultCam.SetActive(true);

        //StartCoroutine(ScoreManagerS.Instance.ScoreAnimation(0, ScoreManagerS.Instance.Score, 10.0f, ScoreText));
        //ScoreText.gameObject.SetActive(true);
        ////Time.timeScale = 0;

        //yield return new WaitForSeconds(1.2f);
        //fadeOut.SetActive(false);
    }
}