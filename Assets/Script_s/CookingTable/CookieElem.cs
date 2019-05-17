using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieElem : MonoBehaviour {

    [Header("クッキーの素")]
    public CookingMaterial doughPre;

    [Header("チョコレート")]
    public CookingMaterial chocolatePre;

    [Header("ジャム")]
    public CookingMaterial jamPre;

    public CookingMaterial[] setCookie;
}
