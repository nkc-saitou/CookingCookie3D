using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CookieStrike : MonoBehaviour {
    //行動パターン
    public enum MovePattern { _Search,_Atack, _Death }
    MovePattern movepattern = MovePattern._Search;

    // 現在位置
    private Vector3 Position;
    //速さ
    private Vector3 speed = new Vector3(0.5f, 0f, 0.5f);
    private float _HP = 1;
    private Vector3 spot;

    // ラジアン
    private float rad;
    //一番近いクッキー
    private GameObject nearestEnemy = null;

    void Update()
    {
        if (_HP <= 0)
        {
            movepattern = MovePattern._Death;
        }
        switch (movepattern)
        {
            case MovePattern._Search:
                Search();
                break;

            case MovePattern._Atack:
                Atack();
                break;

            case MovePattern._Death:
                Death();
                break;
        }
        if (Vector3.Distance(Vector3.zero, transform.position) > 50)
        {
            Destroy(gameObject);
        }
    }
    void Search()
    {
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            movepattern = MovePattern._Atack;
            GameObject[] cookies = null;
            cookies = GameObject.FindGameObjectsWithTag("Enemy").
            OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();
            nearestEnemy = cookies[0];
            spot = nearestEnemy.transform.position;
            rad = Mathf.Atan2(
                spot.z - transform.position.z,
                spot.x - transform.position.x);
            Position = transform.position;
        }
    }

    void Atack()
    {
        Position.x += speed.x * Mathf.Cos(rad);
        Position.z += speed.z * Mathf.Sin(rad);
        transform.position = Position;
    }

    void Death()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<EnemyMove>()._HP--;
            _HP--;
        }
        if (col.tag == "Wall")
        {
            _HP--;
        }
    }
}
