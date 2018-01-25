using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    ScoreManeger SM;

    public GameObject prediction;
    //沸くクッキー
    public GameObject Cookie1;

    //クッキーの沸きパターン
    private int Pattern;
    private int low=1;
    private int max=3;

    //正負ランダム
    private int a;

    private bool clear = false;//クリア

    private bool killed = true;
    public bool[] wallBroken= { false,false,false,false};//東,南,北,西

    private GameObject East;
    private GameObject South;
    private GameObject Noth;
    private GameObject West;

    public float spawntime=3f;
    public float timer=180;

    public bool Killed
    {
        get { return killed; }
        set { killed = value; }
    }

    public bool _Clear
    {
        get { return clear; }
        set { clear = value; }
    }


	void Start () {
        SM = GameObject.FindObjectOfType<ScoreManeger>().GetComponent<ScoreManeger>();
        East = GameObject.Find("East");
        South = GameObject.Find("South");
        Noth = GameObject.Find("Noth");
        West = GameObject.Find("West");
        StartCoroutine("ESpawn");
	}
	

	void Update () {
        if (wallBroken[0] && wallBroken[1] && wallBroken[2] && wallBroken[3])
        {
            //Gameover
            Debug.Log("Gameover");
        }
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else if ((!wallBroken[0] || !wallBroken[1] || !wallBroken[2] || !wallBroken[3]) && timer < 0 && clear == false)
        {
            //Clear
            StageClear();
        }
    }

    private void RandomSpawnECookie()
    {
        if (!clear)
        {
            RandomPM();
            Pattern = Random.Range(low, max);
            switch (Pattern)
            {
                case 1://上下のクッキーの沸き
                    if (wallBroken[1] && wallBroken[2])
                    {
                        low = 2;
                        RandomSpawnECookie();
                        break;
                    }
                    if (wallBroken[1]&&!wallBroken[2])
                    {
                        a = 1;
                    }
                    if (wallBroken[2] && !wallBroken[1])
                    {
                        a = -1;
                    }
                    GameObject Cookie = (GameObject)Instantiate(prediction, new Vector3(0, 2, 14 * a), Quaternion.identity);
                    Prediction PD = Cookie.GetComponent<Prediction>();
                    switch (a)
                    {
                        case -1:
                            Cookie.transform.parent = South.transform;
                            PD.ParentObject = South;
                            break;
                        case 1:
                            Cookie.transform.parent = Noth.transform;
                            PD.ParentObject = Noth;
                            break;
                    }

                    PD.Enemy = Cookie1;
                    break;

                case 2://左右
                    if (wallBroken[0] && wallBroken[3])
                    {
                        max = 2;
                        RandomSpawnECookie();
                        break;
                    }
                    if (wallBroken[0] && !wallBroken[3])
                    {
                        a = -1;
                    }
                    if (wallBroken[3] && !wallBroken[0])
                    {
                        a = 1;
                    }
                    GameObject cooKie = (GameObject)Instantiate(prediction, new Vector3(22 * a, 2, 0), Quaternion.identity);
                    Prediction _PD = cooKie.GetComponent<Prediction>();
                    switch (a)
                    {
                        case -1:
                            cooKie.transform.parent = West.transform;
                            _PD.ParentObject = West;
                            break;
                        case 1:
                            cooKie.transform.parent = East.transform;
                            _PD.ParentObject = East;
                            break;
                    }
                    _PD.Enemy = Cookie1;
                    break;
            }
        }
    }

    private IEnumerator ESpawn()//敵の沸き間隔
    {
        while (!clear)
        {
            yield return new WaitForSeconds(spawntime);
            if (killed)
            {
                killed = false;
                RandomSpawnECookie();
            }
        }
    }

    private void RandomPM()//正負のランダム
    {
        a = Random.Range(0, 2);
        a *= 2;
        a--;
    }
    void StageClear()
    {
        clear = true;
        StopCoroutine("ESpawn");
        for (int i = 0; i < 4; i++)
        {
            if (!wallBroken[i])
            {
                SM.Score += 100;
            }
        }
        SM.AddScore();
        SM.Save();
        Debug.Log("clear");
    }
}
