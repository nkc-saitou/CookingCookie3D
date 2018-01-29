using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    //--------------------------------------------
    // public
    //--------------------------------------------
    
    [Header("敵の目的地")]
    public GameObject enemyWait;

    //--------------------------------------------
    // private
    //--------------------------------------------

    CookieSoldier cookieEnemy;

    void Start ()
    {
        cookieEnemy = transform.parent.GetComponent<CookieSoldier>();
	}
	
	void Update ()
    {
        CookieCreateManager();
    }

    /// <summary>
    /// ３種類のクッキーをランダムで生成
    /// </summary>
    void CookieCreateManager()
    {
        int createRand = UnityEngine.Random.Range(0, cookieEnemy.setCookie.Length);

        //他のクッ菌がいなかったら生成
        if(enemyWait.transform.childCount == 0)
        {
            Instantiate(cookieEnemy.setCookie[createRand].gameObject, transform.position,Quaternion.identity,enemyWait.transform);

        }
    }
}
