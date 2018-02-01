using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour {

    RandomEnemy rand;

    NumberofEnemy numEnemy;

    public GameObject endPoint;

    public WallHP wallHP;

    int randomNum;

    bool stopCreate = false;

    // Use this for initialization
    void Start () {
        rand = transform.parent.GetComponent<RandomEnemy>();
        //numEnemy = GameObject.FindObjectOfType<NumberofEnemy>().GetComponent<NumberofEnemy>();
	}
	
	// Update is called once per frame
	void Update () {


    }

    void FixedUpdate()
    {
        randomNum = Random.Range(0, rand.CreateCookie.Length);

        if (endPoint.transform.childCount == 0 && stopCreate == false)
        {
            Instantiate(rand.CreateCookie[randomNum], gameObject.transform.position, Quaternion.identity, endPoint.transform);
            //rand.createEnemy--;
        }

        if (wallHP.breakFlg == true || rand.createEnemy <= 0 || EnemyDeath._EnemyDeath <= 0)
        {
            //クッキーの生成を止める
            stopCreate = true;

            if (endPoint.transform.childCount != 0)
            {
                Destroy(endPoint.transform.GetChild(0).gameObject, 3.0f);
            }
        }
    }
}
