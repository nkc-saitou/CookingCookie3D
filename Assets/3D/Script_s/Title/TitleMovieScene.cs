using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMovieScene : MonoBehaviour {

    public Animator anim;
    AnimatorStateInfo stateInfo;

    bool filst = true;

    // Use this for initialization
    void Start () {

        AudioManager.Instance.FadeOutBGM();
        StartCoroutine(waitTime());
    }

    // Update is called once per frame
    void Update()
    {
        //AnimatorstateInfoは構造体のため、毎フレーム取得し続ける必要がある
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (Input.GetButtonDown("JoyStick_Action1") && filst)
        {
            filst = false;
            AudioManager.Instance.PlaySE("Button");
            FadeManager.Instance.LoadScene("Title", 0.1f);
        }
        //animの再生が終わっているかどうか
        if (stateInfo.normalizedTime >= 1.0f)
        {
            FadeManager.Instance.LoadScene("Title", 0.1f);
        }

    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(6.0f);
        AudioManager.Instance.PlayBGM("Result");

        yield return new WaitForSeconds(20.0f);
        AudioManager.Instance.FadeOutBGM();

        yield return new WaitForSeconds(8.0f);
        AudioManager.Instance.PlayBGM("movie");

        yield return new WaitForSeconds(46.0f);
        AudioManager.Instance.PlayBGM("oretati");
    }
}
