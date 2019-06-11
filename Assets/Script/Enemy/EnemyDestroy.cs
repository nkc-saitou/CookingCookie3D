using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour {

    CookingMaterial enemyType;
    CookingMaterial cookieType;

    EnemyMove enemyMove;

    bool flg = true;
    [System.NonSerialized]
    public bool imageChangeFlg = false;

    public GameObject humanPre;
    public GameObject effectPre;

	void Start () {
        enemyType = GetComponent<CookingMaterial>();
        enemyMove = GetComponent<EnemyMove>();
	}

    void OnCollisionStay(Collision col)
    {
        StartCoroutine(DestroyCookie(col.gameObject));
    }

    IEnumerator DestroyCookie(GameObject col)
    {
        cookieType = col.GetComponent<CookingMaterial>();

        if (cookieType == null) yield break;

        flg = true;
        if (cookieType.type == enemyType.type && flg)
        {
            flg = false;
            AudioManager.Instance.PlaySE("EnemyDown");
            GameObject effect = Instantiate(effectPre,transform.position,Quaternion.identity, transform.parent);

            Destroy(effect,1.0f);

            EnemyDeath._EnemyDeath -= 1;
            ImageNumber._EnemyCountFlg = true;

            Destroy(col.gameObject);
            Destroy(gameObject);

            GameObject human = Instantiate(humanPre, transform.position, Quaternion.identity,transform.parent);
            Destroy(human, 3.0f);
        }
    }

}