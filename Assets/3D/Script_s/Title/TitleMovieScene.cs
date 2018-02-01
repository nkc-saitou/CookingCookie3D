using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMovieScene : MonoBehaviour {

    public Animator anim;
    AnimatorStateInfo stateInfo;

    // Use this for initialization
    void Start () {
        AudioManager.Instance.PlayBGM("starWars");
    }

    // Update is called once per frame
    void Update()
    {
        //AnimatorstateInfoは構造体のため、毎フレーム取得し続ける必要がある
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (Input.GetButtonDown("JoyStick_Action1"))
        {
            FadeManager.Instance.LoadScene("Title", 0.1f);
        }
        //animの再生が終わっているかどうか
        if (stateInfo.normalizedTime >= 1.0f)
        {
            FadeManager.Instance.LoadScene("Title", 0.1f);
        }

    }
}
