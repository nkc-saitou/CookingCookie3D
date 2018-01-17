using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public GameObject prediction;
    //沸くクッキー
    public GameObject Cookie1;
    //クッキーの沸きパターン
    private int Pattern;
    //正負ランダム
    private int a;
    public float spawntime=5f;
	void Start () {
        StartCoroutine("ESpawn");
	}
	

	void Update () {
	}

    private void RandomSpawnECookie()
    {
        RandomPM();
        switch (Pattern = Random.Range(1, 3))
        {
            case 1://上下のクッキーの沸き
                GameObject Cookie = Instantiate(prediction, new Vector3(Random.Range(-20.0f, 20.0f), 2,  12* a), Quaternion.identity);
                Prediction PD = Cookie.GetComponent<Prediction>();
                PD.Enemy = Cookie1;
                break;

            case 2://左右
                GameObject cooKie = Instantiate(prediction, new Vector3(20 * a, 2, Random.Range(-12f, 12f)), Quaternion.identity);
                Prediction _PD = cooKie.GetComponent<Prediction>();
                _PD.Enemy = Cookie1;
                break;
        }
    }

    private IEnumerator ESpawn()//敵の沸き間隔
    {
        for(int i = 0; i < 36; i++)
        {
            yield return new WaitForSeconds(spawntime);
            RandomSpawnECookie();
        }
    }

    private void RandomPM()//正負のランダム
    {
        a = Random.Range(0, 2);
        a *= 2;
        a--;
    }
}
