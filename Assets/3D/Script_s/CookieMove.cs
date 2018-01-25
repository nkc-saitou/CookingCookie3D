using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class CookieMove : MonoBehaviour {

    //行動パターン
    public enum MovePattern { _Wait, _Search, _Stalking, _Atack, _Death }
    MovePattern movepattern = MovePattern._Wait;

    public enum CookieType { E_Normal, E_Chocolate, E_Jam }
    CookieType cookieType;

    NavMeshAgent NMA;

    //視界に入っているか
    private bool vision;
    private Ray ray;
    private RaycastHit rayhit;
    //攻撃開始距離
    private int distance = 5;

    // 現在位置
    private Vector3 Position;


    //体当たり速さ
    private Vector3 speed = new Vector3(0.5f, 0f, 0.5f);
    //回転速
    private float rotsp = 8f;

    // ラジアン
    private float rad;

    //一番近いクッキー
    private GameObject nearestEnemy = null;

    //クッキーが攻撃範囲内
    private bool atackarea = false;
    //待ち
    private bool wait = true;
    private float time = 0.08f;

    void Start()
    {
        NMA = GetComponent<NavMeshAgent>();
        switch (cookieType)
        {
            case CookieType.E_Normal:
                tag = "C_Normal";
                break;
            case CookieType.E_Chocolate:
                tag = "C_Chocolate";
                break;
            case CookieType.E_Jam:
                tag = "C_Jam";
                break;     
        }
    }


    void Update()
    {
        SearchEnemy();
        AtackFlag();

        switch (movepattern)
        {
            case MovePattern._Wait:
                if (wait)
                {
                    Invoke("Wait", time);
                    break;
                }
                break;

            case MovePattern._Search:
                wait = true;
                if (GameObject.FindGameObjectWithTag(cookieType.ToString()))
                {
                    atackarea = false;
                    movepattern = MovePattern._Stalking;
                    NMA.stoppingDistance = 0f;
                    NMA.autoBraking = false;
                    break;
                }
                /*else if (GameObject.FindGameObjectWithTag("Prediction"))
                {
                    atackarea = false;
                    movepattern = MovePattern._Stalking;
                    NMA.stoppingDistance = 2f;
                    NMA.autoBraking = false;
                    break;
                }*/
                Search();
                break;


            case MovePattern._Stalking:
                if (atackarea&&vision)
                {
                    movepattern = MovePattern._Atack;
                    NMA.isStopped = true;
                    break;
                }
                if (!GameObject.FindGameObjectWithTag((cookieType.ToString())))
                {
                    movepattern = MovePattern._Wait;
                    break;
                }
                Stalking();
                break;


            case MovePattern._Atack:
                if (!GameObject.FindGameObjectWithTag((cookieType.ToString())))
                {
                    movepattern = MovePattern._Wait;
                    break;
                }
                else if (!atackarea && !vision)
                {
                    movepattern = MovePattern._Stalking;
                }
                Atack();
                break;


            case MovePattern._Death:
                Death();
                break;
        }
    }

    void SearchEnemy()
    {
        if (GameObject.FindGameObjectWithTag((cookieType.ToString())))
        {
            GameObject[] cookies = null;
            cookies = GameObject.FindGameObjectsWithTag((cookieType.ToString())).
            OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();
            nearestEnemy = cookies[0];
        }
        else if (GameObject.FindGameObjectWithTag("Prediction"))
        {
            GameObject[] cookies = null;
            cookies = GameObject.FindGameObjectsWithTag("Prediction").
            OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();
            nearestEnemy = cookies[0];
        }
    }

    void AtackFlag()
    {
        if (nearestEnemy!=null)
        {
            Vector3 raydirection = (nearestEnemy.transform.position - transform.position).normalized;
            ray = new Ray(transform.position, raydirection);
            if (Physics.Raycast(ray, out rayhit, distance))
            {
                if (rayhit.collider.tag == (cookieType.ToString()))
                {
                    vision = true;
                }
                else
                {
                    vision = false;
                }
            }
            if ((nearestEnemy.transform.position - transform.position).magnitude <= distance)
            {
                atackarea = true;
            }
            else
            {
                atackarea = false;
            }
        }
    }
    void Wait()
    {
        wait = false;
        NMA.ResetPath();
        NMA.isStopped = false;
        movepattern = MovePattern._Search;
    }

    void Search()
    {

    }
    void Stalking()
    {
        if (nearestEnemy == null)
        {
            return;
        }
        NMA.speed = 4;
        NMA.SetDestination(nearestEnemy.transform.position);
    }

    void Atack()
    {
        if (nearestEnemy != null)
        {
            Vector3 dir = new Vector3(nearestEnemy.transform.position.x, transform.position.y, nearestEnemy.transform.position.z) - transform.position;
            Vector3 newdir = Vector3.RotateTowards(transform.forward, dir, rotsp * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newdir);
            rad = Mathf.Atan2(
                nearestEnemy.transform.position.z - transform.position.z,
                nearestEnemy.transform.position.x - transform.position.x);
            Position = transform.position;
            Position.x += speed.x * Mathf.Cos(rad);
            Position.z += speed.z * Mathf.Sin(rad);
            transform.position = Position;
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == (cookieType.ToString()))
        {
            /*NMA.isStopped = true;
            vision = false;
            atackarea = false;
            movepattern = MovePattern._Wait;*/
            col.GetComponent<EnemyMove>()._HP--;
            Death();
        }
    }
}

