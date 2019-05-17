using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour {

    RandomEnemy rand;

    //壁際で止まる位置
    public GameObject endPoint;

    //壁のHP
    public WallHP wallHP;

    //敵をランダムで出現させる
    int randomNum;

    //生成を止めるためのフラグ
    bool stopCreate = false;

    //生成するのは一度だけ
    bool createFilst = true;

    void Start () {
        rand = transform.parent.GetComponent<RandomEnemy>();
	}

    void FixedUpdate()
    {
        //敵の種類をランダムで選ぶ
        randomNum = Random.Range(0, rand.CreateCookie.Length);

        //クッキーがおらず、出せる状態のとき
        if (endPoint.transform.childCount == 0 && stopCreate == false && createFilst)
        {
            createFilst = false;

            StartCoroutine(EnemyWaitTime());
        }

        //ゲームオーバー、ゲームクリア時
        if (wallHP.breakFlg == true ||EnemyDeath._EnemyDeath <= 0)
        {
            //クッキーの生成を止める
            stopCreate = true;

            //敵クッキーがいる状態だったら
            if(endPoint.transform.childCount != 0)
            {
                foreach(Transform child in endPoint.transform)
                {
                    child.transform.parent = null;
                }
            }
        }
    }

    /// <summary>
    /// クッキーを作るための処理
    /// </summary>
    /// <returns></returns>
    IEnumerator EnemyWaitTime()
    {
        //生成のタイミングを一瞬遅らせる
        yield return new WaitForSeconds(0.5f);

        //クッキーを生成
        Instantiate(rand.CreateCookie[randomNum], gameObject.transform.position, Quaternion.identity, endPoint.transform);
        createFilst = true;
    }
}
