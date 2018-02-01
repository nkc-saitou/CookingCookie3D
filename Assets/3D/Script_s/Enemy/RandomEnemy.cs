using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : MonoBehaviour {

    [Header("エネミークッキー")]
    public GameObject[] enemyCookie;

    ////public int MaxEnemy=50;
    public int createEnemy = 50;

    public int CreateEnemy
    {
        get { return createEnemy; }
        set { createEnemy = value; }
    }

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