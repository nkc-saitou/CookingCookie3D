using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMove : MonoBehaviour {

    GameObject parentObj;

    public float speed = 0.1f;
    private Vector3 vec;

    void Start ()
    {
		
	}

    void Update()
    {
        parentObj = transform.parent.gameObject.transform.GetChild(0).gameObject;

        transform.rotation = 
            Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(parentObj.transform.localPosition - transform.localPosition), 0.3f);

        transform.localPosition += transform.forward * speed;



    }
}