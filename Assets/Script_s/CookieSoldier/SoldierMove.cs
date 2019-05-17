using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMove : MonoBehaviour {

    //親オブジェクト
    GameObject EnemyCookieObj;

    //移動スピード
    public float speed = 0.1f;

    void Update()
    {
        if (transform.parent != null)
        {
            //敵クッキー
            EnemyCookieObj = transform.parent.gameObject.transform.GetChild(0).gameObject;

            //敵クッキーの方向を向く
            transform.rotation =
                Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(EnemyCookieObj.transform.localPosition - transform.localPosition), 0.3f);

            //移動
            transform.localPosition += transform.forward * speed;
        }
    }
}