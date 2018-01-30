using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour {

    RandomEnemy rand;

    public GameObject endPoint;

    public WallHP wallHP;

    int randomNum;

    bool stopCreate = false;

    // Use this for initialization
    void Start () {
       rand = transform.parent.GetComponent<RandomEnemy>();
	}
	
	// Update is called once per frame
	void Update () {



        randomNum = Random.Range(0, rand.CreateCookie.Length);


		if(endPoint.transform.childCount == 0 && stopCreate == false)
        {
            Instantiate(rand.CreateCookie[randomNum], gameObject.transform.position, Quaternion.identity, endPoint.transform);
        }

        if(wallHP.breakFlg == true)
        {
            //クッキーの生成を止める
            stopCreate = true;
        }
    }
}
