using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour {

    public PlayerMove playerMove;

    public bool walk = false;
    public bool hold = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(playerMove.animWalkFlg)
        {
            GetComponent<Animator>().SetBool("walk", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("walk", false);
        }

        if(hold == true)
        {
            GetComponent<Animator>().SetBool("hold", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("hold", false);
        }
    }
}
