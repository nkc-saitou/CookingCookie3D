using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public enum SelectType
{
    Title = 0,
    EasyStage,
    NormalStage,
    HardStage
}

public class StageSelect : MonoBehaviour {

    public SelectType select;

    string sceneName;

    bool filst = true;

    public Image stageMovie;
    public Image gameStartImg;

    [Header("ゲーム開始のスプライト")]
    public Sprite startSp;

    [Header("プレイヤーエントリーのスプライト")]
    public Sprite playerEntry;

    [Header("ステージのスプライト")]
    public Sprite sprite;

    public Animator anim;

    [Header("次のシーンに遷移するとき")]
    public GameObject fadeIN;

    void Start()
    {

    }

    void Update ()
    {

	}

    void OnTriggerStay(Collider other)
    {
        switch (select)
        {
            case SelectType.Title:
                sceneName = "Title";
                break;

            case SelectType.EasyStage:
                sceneName = "Main_Easy";
                break;

            case SelectType.NormalStage:
                sceneName = "Main";
                break;

            case SelectType.HardStage:
                sceneName = "Main_Hard";
                break;
        }
        if (Input.GetButtonDown("JoyStick_Action1") && filst)
        {
            filst = false;
            AudioManager.Instance.PlaySE("Button");
            fadeIN.SetActive(true);
            StartCoroutine(WaitFadeIn(sceneName));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //タイトルへ行くボタン以外だったら
        if (select != SelectType.Title)
        {
            anim.SetBool("IsStop",false);
            gameStartImg.sprite = startSp;
            stageMovie.sprite = sprite;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (select != SelectType.Title)
        {
            anim.SetBool("IsStop", true);
            gameStartImg.sprite = playerEntry;
        }
    }

    /// <summary>
    /// フェードするときの調整用
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitFadeIn(string name)
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(name);
    }
}
