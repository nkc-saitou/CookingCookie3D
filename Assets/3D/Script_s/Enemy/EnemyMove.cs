using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
public class EnemyMove : MonoBehaviour {

    //行動パターン
    public enum MovePattern {_Stalking,_Wall,_NormalMove,_CookieAtack,_Death}
    MovePattern movepattern=MovePattern._NormalMove;

    public enum OpponentType { C_Normal, C_Chocolate, C_Jam }
    OpponentType opponentType;

    private EnemySpawn ES;

    private Vector3 raydirection;
    private Ray ray;
    private RaycastHit rayhit;
    private int distance=10;//視認距離
    private int layerMask = ~(1 << 11);

    private int _direction;        //0:東 1:南 2:北 3:西

    private float rotsp = 8;
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
    //壁殴り中しているか
    private bool wallAtack = true;



    public int _Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    void Start() {
        ES = GameObject.Find("EnemySpawn").GetComponent<EnemySpawn>();
        transform.LookAt(Vector3.zero);
        int RandomType;
        switch (RandomType = Random.Range(0, 3))
        {
            case 0:
                opponentType = OpponentType.C_Normal;
                tag = "E_Normal";
                break;
            case 1:
                opponentType = OpponentType.C_Chocolate;
                tag = "E_Chocolate";
                break;
            case 2:
                opponentType = OpponentType.C_Jam;
                tag = "E_Jam";
                break;
        }
    }

    void Update()
    {
        if (!ES._Clear)
        {
            if (_HP <= 0)
            {
                //movepattern = MovePattern._Death;
            }
            SearchCookies();

            switch (movepattern)
            {
                case MovePattern._Stalking:
                    if (nearestCookie == null)
                    {
                        movepattern = MovePattern._NormalMove;
                        break;
                    }
                    else if (nearflg)
                    {
                        movepattern = MovePattern._CookieAtack;
                        break;
                    }
                    Stalking();
                    break;


                case MovePattern._NormalMove:
                    if (nearestCookie != null)
                    {
                        raydirection = (nearestCookie.transform.position - transform.position).normalized;
                        ray = new Ray(transform.position, raydirection);
                        if (Physics.Raycast(ray, out rayhit, distance, layerMask))
                        {
                            if (rayhit.collider.tag == opponentType.ToString())
                            {
                                movepattern = MovePattern._Stalking;
                                break;
                            }
                        }
                    }
                    else if (wall)
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
                        if (Physics.Raycast(ray, out rayhit, distance, layerMask))
                        {
                            if (rayhit.collider.tag == opponentType.ToString())
                            {
                                movepattern = MovePattern._Stalking;
                                break;
                            }
                        }
                    }
                    else if (wall == false)
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
                    //Death();
                    break;
            }
        }
    }
    
    //クッキー探査、地雷クッキーが優先される
    void SearchCookies()
    {
        if (GameObject.FindGameObjectWithTag(opponentType.ToString()))
        {
            GameObject[] cookie = null;
            cookie = GameObject.FindGameObjectsWithTag(opponentType.ToString()).
            OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();
            if ((cookie[0].transform.position - transform.position).magnitude <= 12)
            {
                nearestCookie = cookie[0];
            }
            else
            {
                nearestCookie = null;
            }
        }
    }



    //クッキーを追跡
    void Stalking()
    {
        Vector3 dir = new Vector3(nearestCookie.transform.position.x, transform.position.y, nearestCookie.transform.position.z) - transform.position;
        Vector3 newdir = Vector3.RotateTowards(transform.forward, dir, rotsp * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newdir);
        rad = Mathf.Atan2(
                nearestCookie.transform.position.z - transform.position.z,
                nearestCookie.transform.position.x - transform.position.x);
        Position = transform.position;
        Position.x += speed.x * Mathf.Cos(rad);
        Position.z += speed.z * Mathf.Sin(rad);
        transform.position = Position;
    }

    //壁(原点)に向かう
    void NormalMove()
    {
        Vector3 dir = new Vector3(0,transform.position.y,0) - transform.position;
        Vector3 newdir = Vector3.RotateTowards(transform.forward, dir, rotsp * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newdir);
        rad = Mathf.Atan2(
               0 - transform.position.z,
               0 - transform.position.x);
       Position = transform.position;
       Position.x += speed.x * Mathf.Cos(rad);
       Position.z += speed.z * Mathf.Sin(rad);
       transform.position = Position;
    }

    //壁
    void Wall()
    {
        if (transform.position.x > 17f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,-90,0), Time.deltaTime);
        }
        else if (transform.position.x < -17f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime);
        }
        else if (transform.position.z > 9.5f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime);
        }
        else if (transform.position.x < -8f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(180, 180, 180), Time.deltaTime);
        }
        if (wallAtack)
        {

            wallAtack = false;
            StartCoroutine("WallAtack");
        }
    }

    //クッキーを攻撃
    void CookieAtack()
    {
        
    }

    //死亡処理
    public void Death()
    {
        //GameObject.FindObjectOfType<ScoreManeger>().GetComponent<ScoreManeger>().Score += 10;
        //GameObject.Find("EnemySpawn").GetComponent<EnemySpawn>().killed[_direction] = true;
        GameObject.FindObjectOfType<NumberofEnemy>().GetComponent<NumberofEnemy>().EnemyNumber--;
        Destroy(gameObject);
    }


    //壁殴り
    private IEnumerator WallAtack()
    {
        yield return new WaitForSeconds(3f);
        if (!ES._Clear)
        {
            if (GameObject.FindGameObjectWithTag("FactoryWall"))
            {
                GameObject[] wallObject = null;
                wallObject = GameObject.FindGameObjectsWithTag("FactoryWall").
                OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();
                wallObject[0].GetComponent<WallHP>()._HP -= 1;
                wallAtack = true;
            }
        }
    }




void OnTriggerEnter(Collider col)
    {
        if (col.tag == opponentType.ToString())
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
        if (col.tag == opponentType.ToString())
        {
            nearflg = false;
        }

        if (col.tag == "Wall")
        {
            wall = false;
        }
    }
}
