using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTableSetUV : MonoBehaviour {

    public float scrollSpeed = 0.5f;
    public Renderer rend;

    bool offsetflg;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            offsetflg = !offsetflg;
        }

        if (offsetflg)
        {
            float offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));
        }
	}
}
