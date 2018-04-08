using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookieSoldier : MonoBehaviour {

    //作った味方クッキー
    [Header("クッキー兵士(ノーマル、チョコ、ジャム)")]
    public GameObject[] setCookie;

    //答えクッキーを視覚化するための画像
    [Header("ノーマル、チョコ、ジャム")]
    public Sprite[] answerImage;
}
