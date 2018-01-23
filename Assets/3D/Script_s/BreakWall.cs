using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour {

    public bool breakflg;

    float pow = 100.0f;
    Rigidbody rg;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (breakflg == true) Break();
	}

    void Break()
    {
        foreach(Transform wall in GetComponentInChildren<Transform>())
        {
            rg = wall.gameObject.AddComponent<Rigidbody>();
            if (rg != null)
            {
                rg.isKinematic = false;
                rg.useGravity = true;
                rg.AddExplosionForce(pow, Vector3.forward, 0f);
                breakflg = false;
            }

        }

    }
}
