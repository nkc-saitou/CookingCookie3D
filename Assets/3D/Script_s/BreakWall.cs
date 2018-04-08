using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour {

    //壁のHP
    WallHP wallHP;

    //飛ばす力
    float pow = 100.0f;
    Rigidbody rg;

	void Start () {
        wallHP = transform.parent.GetComponent<WallHP>();
    }
	
	void Update () {

        if (wallHP.breakFlg == true)
        {
            Break();
        }
	}

    void Break()
    {
        //子オブジェクトを取得
        foreach (Transform wall in GetComponentInChildren<Transform>())
        {
            rg = wall.GetComponent(typeof(Rigidbody)) as Rigidbody;

            //Rigidbodyをアタッチ
            if (rg == null) wall.gameObject.AddComponent<Rigidbody>();

            if (rg != null)
            {
                rg.isKinematic = false;
                rg.useGravity = true;

                //飛ばす
                rg.AddExplosionForce(pow, Vector3.forward, 0f);

                wallHP.breakFlg = false;
            }
        }
    }
}

