using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneBGM : MonoBehaviour {

    [Header("次のシーンに遷移するとき")]
    public GameObject fadeIN;

    [Header("違うシーンから遷移してきたとき")]
    public GameObject fadeOut;

    public GameObject create;
    public Image createSp;
    public Sprite sp;

    bool inputFlg = false;

    public GameObject startAnim;

    bool filst = true;


    // Use this for initialization
    void Start () {
        PlayerMoveSetting.Instance.MoveSettingFlg = false;

        create.SetActive(true);

        fadeOut.SetActive(false);
        fadeOut.SetActive(true);

        StartCoroutine(WaitFadeOut());
        startAnim.SetActive(false);
        AudioManager.Instance.PlayBGM("game");
	}
	
	// Update is called once per frame
	void Update () {

        StartCoroutine(WaitCreate());
	}

    /// <summary>
    /// 遷移してきたときの調整用
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitFadeOut()
    {
        yield return new WaitForSeconds(2.5f);
        fadeOut.SetActive(false);
        Time.timeScale = 0;
        inputFlg = true;
    }

    IEnumerator WaitCreate()
    {
        if (inputFlg == false) yield break;

        if(Input.GetButtonDown("JoyStick_Action1") && filst)
        {
            createSp.sprite = sp;
            filst = false;
        }
        else if (Input.GetButtonDown("JoyStick_Action1") && filst == false)
        {
            Time.timeScale = 1;
            create.SetActive(false);
            startAnim.SetActive(true);
        }

        yield return new WaitForSeconds(2.0f);
        PlayerMoveSetting.Instance.MoveSettingFlg = true;
    }
}