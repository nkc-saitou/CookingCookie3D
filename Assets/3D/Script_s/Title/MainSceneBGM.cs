using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneBGM : MonoBehaviour {

    [Header("次のシーンに遷移するとき")]
    public GameObject fadeIN;

    [Header("違うシーンから遷移してきたとき")]
    public GameObject fadeOut;

    public GameObject startAnim;

    // Use this for initialization
    void Start () {
        PlayerMoveSetting.Instance.MoveSettingFlg = false;

        fadeOut.SetActive(false);
        fadeOut.SetActive(true);

        startAnim.SetActive(false);
        StartCoroutine(WaitFadeOut());
        AudioManager.Instance.PlayBGM("game");
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
        startAnim.SetActive(true);

        yield return new WaitForSeconds(2.0f);
        PlayerMoveSetting.Instance.MoveSettingFlg = true;
    }
}