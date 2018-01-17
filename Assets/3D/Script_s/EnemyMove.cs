using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
public class EnemyMove : MonoBehaviour {

    //行動パターン
    public enum MovePattern {_Stalking,_Wall,_NormalMove,_CookieAtack,_Death}
    MovePattern movepattern=MovePattern._NormalMove;

    private Vector3 raydirection;
    private Ray ray;
    private RaycastHit rayhit;
    private int distance=10;

    // 現在位置
    private Vector3 Position;
    public int _HP = 1;
    //速さ
    private Vector3 speed=new Vector3(0.04f,0f,0.04f);

    // ラジアン
    private float rad;
    //一番近いクッキー
    private GameObject nearestCookie=null;

    //クッキーに接触しているか
    private bool nearflg = false;
    //クッキーが視界にいるか
    private bool vision;
    //工場入口についたかどうか
    private bool wall = false;

    public GameObject NearestCookie
    {
        get { return nearestCookie; }
        set { nearestCookie = value; }
    }

    void Start () {
    }

    void Update()
    {
        if (_HP <= 0)
        {
            movepattern = MovePattern._Death;
        }

        switch (movepattern)
        {
            case MovePattern._Stalking:
                if (nearestCookie == null)
                {
                    movepattern = MovePattern._NormalMove;
                    break;
                }
                if (nearflg)
                {
                    movepattern = MovePattern._CookieAtack;
                    break;
                }
                Vector3 raydirection = (nearestCookie.transform.position - transform.position).normalized;
                ray = new Ray(transform.position, raydirection);
                if (Physics.Raycast(ray, out rayhit, distance))
                {
                    if (rayhit.collider.tag == "Cookie" || rayhit.collider.tag == "CookieMine")
                    {
                        Stalking();
                        break;
                    }
                    else
                    {
                        movepattern = MovePattern._NormalMove;
                        break;
                    }
                }
                Stalking();
                break;

            case MovePattern._NormalMove:
                if (nearestCookie != null)
                {
                    raydirection = (nearestCookie.transform.position - transform.position).normalized;
                    ray = new Ray(transform.position, raydirection);
                    if (Physics.Raycast(ray, out rayhit, distance))
                    {
                        if (rayhit.collider.tag == "Cookie"||rayhit.collider.tag=="CookieMine")
                        {
                            movepattern = MovePattern._Stalking;
                            break;
                        }
                    }
                }
                if (wall)
                {
                    movepattern = MovePattern._Wall;
                    break;
                }
                NormalMove();
                break;

            case MovePattern._Wall:
                if (nearestCookie != null)
                {
                    raydirection = (nearestCookie.transform.position - transform.position).normalized;
                    ray = new Ray(transform.position, raydirection);
                    if (Physics.Raycast(ray, out rayhit, distance))
                    {
                        if (rayhit.collider.tag == "Cookie" || rayhit.collider.tag == "CookieMine")
                        {
                            movepattern = MovePattern._Stalking;
                            break;
                        }
                    }
                }
                if (wall == false)
                {
                    movepattern = MovePattern._NormalMove;
                    break;
                }
                Wall();
                break;

            case MovePattern._CookieAtack:
                if (nearflg == false)
                {
                    movepattern = MovePattern._NormalMove;
                    break;
                }
                CookieAtack();
                break;

            case MovePattern._Death:
                Death();
                break;
        }

    }
    
    void Stalking()
    {
            rad = Mathf.Atan2(
                nearestCookie.transform.position.z - transform.position.z,
                nearestCookie.transform.position.x - transform.position.x);
        Position = transform.position;
        Position.x += speed.x * Mathf.Cos(rad);
        Position.z += speed.z * Mathf.Sin(rad);
        transform.position = Position;
    }

    void NormalMove()
    {
           rad = Mathf.Atan2(
               0 - transform.position.z,
               0 - transform.position.x);
       Position = transform.position;
       Position.x += speed.x * Mathf.Cos(rad);
       Position.z += speed.z * Mathf.Sin(rad);
       transform.position = Position;
    }

    void Wall()
    {
        
    }

    void CookieAtack()
    {
        
    }

    void Death()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Cookie"||col.tag=="CookieMine")
        {
            nearflg = true;
        }
        if (col.tag == "Wall")
        {
            wall = true;
        }
        
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Cookie"||col.tag=="CookieMine")
        {
            nearflg = false;
        }

        if (col.tag == "Wall")
        {
            wall = false;
        }
    }
}
