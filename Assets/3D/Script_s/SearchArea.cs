using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SearchArea : MonoBehaviour {
    public enum Area { E_Area,C_Area}
    public Area area;

    private EnemyMove EM;
    private CookieMove CM;

    private bool mine = false;
	void Start () {
        switch (area)
        {
            case Area.C_Area:
                CM = GetComponentInParent<CookieMove>();
                break;

            case Area.E_Area:
                EM = GetComponentInParent<EnemyMove>();
                break;
        }
	}
	void Update()
    {
        switch (area)
        {
            case Area.C_Area:
                if (GameObject.FindGameObjectWithTag("Enemy"))
                {
                    GameObject[] cookies = null;
                    cookies = GameObject.FindGameObjectsWithTag("Enemy").
                    OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();
                    CM.NearestEnemy = cookies[0];
                    
                }
                else if (GameObject.FindGameObjectWithTag("Prediction")){
                    GameObject[] cookies = null;
                    cookies = GameObject.FindGameObjectsWithTag("Prediction").
                    OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();
                    CM.NearestEnemy = cookies[0];
                    break;
                }
                break;

            case Area.E_Area:
                if (GameObject.FindGameObjectWithTag("CookieMine"))
                {
                    GameObject[] cookiemine = null;
                    cookiemine = GameObject.FindGameObjectsWithTag("CookieMine").
                    OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();
                    if ((cookiemine[0].transform.position-transform.position).magnitude<=12)
                    {
                        EM.NearestCookie = cookiemine[0];
                        mine = true;
                    }
                    else
                    {
                        cookiemine[0] = null;
                        EM.NearestCookie = null;
                        mine = false;
                    }
                    break;
                }
                break;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        switch (area)
        {
            case Area.C_Area:
                if (col.name == "EArea")
                {
                    CM.Atackarea = true;
                    break;
                }
                break;
            case Area.E_Area:
                if (col.name == "CArea"&&mine==false || col.tag == "Cookie"&&mine==false)
                {
                    break;
                }
                break;
        }
    }
    void OnTriggerStay(Collider col)
    {
        switch (area)
        {
            case Area.C_Area:
                if (col.name == "EArea")
                {
                    CM.Atackarea = true;
                    break;
                }
                break;
            case Area.E_Area:
                if (col.name == "CArea"&&mine==false||col.tag=="Cookie"&&mine==false)
                {
                    GameObject[] cookies = null;
                    cookies = GameObject.FindGameObjectsWithTag("Cookie").
                    OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();
                    EM.NearestCookie = cookies[0];
                    break;
                }
                break;
        }
        
    }
    void OnTriggerExit(Collider col)
    {
        switch (area)
        {
            case Area.C_Area:
                if (col.name == "EArea")
                {
                    CM.Atackarea = false;
                    break;
                }
                break;
            case Area.E_Area:
                if (col.name == "CArea" || col.tag == "Cookie")
                {
                    EM.NearestCookie = null;
                    break;
                }
                break;
        }
        
    }
}
