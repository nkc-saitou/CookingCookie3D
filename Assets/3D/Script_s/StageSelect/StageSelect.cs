using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour {

    [Header("次のシーンに遷移するとき")]
    public GameObject fadeIN;

    void Update ()
    {
		
	}

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("JoyStick_Action1"))
        {

            fadeIN.SetActive(true);
            StartCoroutine(WaitFadeIn());

        }
    }

    /// <summary>
    /// フェードするときの調整用
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitFadeIn()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("main");
    }


}
