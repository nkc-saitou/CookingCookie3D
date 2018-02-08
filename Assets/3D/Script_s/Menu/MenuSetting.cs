using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSetting : MonoBehaviour {

    public GameObject menu;

    bool filst = true;

    //メニュー表示中かどうか
    bool menuFlg = false;

    bool menuTimeFlg = false;

    [Header("次のシーンに遷移するとき")]
    public GameObject fadeIN;

    public Button startButton;

    string sceneName;

    // Use this for initialization
    void Start () {
        menu.SetActive(false);

        startButton.Select();
    }
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown("JoyStick_menu") && menuFlg == false)
        {
            Debug.Log("ok");
            menu.SetActive(true);
            Time.timeScale = 0;
            menuFlg = true;
        }
        else if(Input.GetButtonDown("JoyStick_menu") && menuFlg)
        {
            menu.SetActive(false);
            Time.timeScale = 1;
            menuFlg = false;
        }

        if(menuTimeFlg)
        {
            Time.timeScale = 1;
        }

    }

    public void OnTitle()
    {
        Time.timeScale = 1;
        sceneName = "Title";
        menuTimeFlg = true;
        StartCoroutine(WaitFadeIn());
    }

    public void OnStageSelect()
    {
        Time.timeScale = 1;
        sceneName = "StageSelect";
        menuTimeFlg = true;
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
            fadeIN.SetActive(true);

            AudioManager.Instance.PlaySE("Button");
            yield return new WaitForSeconds(2.5f);
            SceneManager.LoadScene(sceneName);
        }
    }

}
