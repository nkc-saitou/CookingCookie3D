using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectFade : MonoBehaviour {

    [Header("違うシーンから遷移してきたとき")]
    public GameObject fadeOut;

    void Start()
    {
        fadeOut.SetActive(true);
        StartCoroutine(WaitFadeOut());
    }

	
	// Update is called once per frame
	void Update () {
		
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
