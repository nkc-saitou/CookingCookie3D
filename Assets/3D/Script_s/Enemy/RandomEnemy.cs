using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : MonoBehaviour {

    [Header("エネミークッキー")]
    public GameObject[] enemyCookie;

    public GameObject[] CreateCookie
    {
        get { return enemyCookie; }
        private set {  enemyCookie = value; }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //enemyRand = Random.Range(0, enemyCookie.Length);
	}


}
