using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMovie : MonoBehaviour {

    float time = 0;

    [Header("次のシーンに遷移するとき")]
    public GameObject fadeIN;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        if (time < 10)
        {
            time += 1f * Time.deltaTime;
        }

        if (time >= 10)
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
        SceneManager.LoadScene("TitleMovie");
    }
}
