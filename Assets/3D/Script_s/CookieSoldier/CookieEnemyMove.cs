using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieEnemyMove : MonoBehaviour {

    GameObject parentObj;

    public float speed = 0.1f;
    private Vector3 vec;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        parentObj = transform.parent.gameObject;

        transform.rotation =
            Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(parentObj.transform.localPosition - transform.localPosition), 0.3f);

        transform.localPosition += transform.forward * speed;
    }
}
