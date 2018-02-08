using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SelectType
{
    Title = 0,
    NormalStage,
    HardStage
}

public class StageSelect : MonoBehaviour {

    public SelectType select;

    string sceneName;

    bool filst = true;

    [Header("次のシーンに遷移するとき")]
    public GameObject fadeIN;

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

            case SelectType.NormalStage:
                sceneName = "Main";
                break;

            case SelectType.HardStage:
                sceneName = "Main_hard";
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
