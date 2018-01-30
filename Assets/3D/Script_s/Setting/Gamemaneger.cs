using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemaneger : MonoBehaviour {

    private bool GameClearFlg = false;
    private WallHP[] wallHp = new WallHP[4];
    private RandomEnemy rand;

    public GameObject[] _Wall=new GameObject[4];
    public GameObject[] endPoint=new GameObject[4];

    private bool call=true;



	void Start () {
        for(int i = 0; i < 4; i++)
        {
            wallHp[i] = _Wall[i].GetComponent<WallHP>();
        }
        rand = GameObject.Find("CreatePos").GetComponent<RandomEnemy>();
	}
	

	void Update () {
        if (rand.MaxEnemy <= 0&&
            endPoint[0].transform.childCount == 0&&
            endPoint[1].transform.childCount == 0&&
            endPoint[2].transform.childCount == 0&&
            endPoint[3].transform.childCount == 0)
        {
            GameClearFlg = true;
        }

        if(wallHp[0]._HP<=0&&
           wallHp[1]._HP <= 0 &&
           wallHp[2]._HP <= 0 &&
           wallHp[3]._HP <= 0)
        {
            //Gameover
        }

        if (GameClearFlg&&call)
        {
            call = false;
            int scenescore = 0;
            for(int j = 0; j < 4; j++)
            {
                scenescore += (int)wallHp[j]._HP*10;
            }
            GameObject.FindObjectOfType<ScoreManeger>().GetComponent<ScoreManeger>().Score += scenescore;
        }
	}
}
