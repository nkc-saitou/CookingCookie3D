using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : MonoBehaviour {

    [Header("エネミークッキー")]
    public GameObject[] enemyCookie;

    //作れるクッキーの種類
    public GameObject[] CreateCookie
    {
        get { return enemyCookie; }
        private set {  enemyCookie = value; }
    }
}