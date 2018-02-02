using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour {

    //public bool breakflg;

    WallHP wallHP;

    float pow = 100.0f;
    Rigidbody rg;

	// Use this for initialization
	void Start () {
        wallHP = transform.parent.GetComponent<WallHP>();

    }
	
	// Update is called once per frame
	void Update () {
        if (wallHP.breakFlg == true)
        {
            Break();
        }
	}

    void Break()
    {
        foreach (Transform wall in GetComponentInChildren<Transform>())
        {
            rg = wall.GetComponent(typeof(Rigidbody)) as Rigidbody;

            if (rg == null) wall.gameObject.AddComponent<Rigidbody>();

            if (rg != null)
            {
                rg.isKinematic = false;
                rg.useGravity = true;
                rg.AddExplosionForce(pow, Vector3.forward, 0f);
                wallHP.breakFlg = false;
            }
        }
    }
}

