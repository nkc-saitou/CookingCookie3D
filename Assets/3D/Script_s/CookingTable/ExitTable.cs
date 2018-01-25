using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTable : MonoBehaviour,IExecutable
{
    CookieSoldier soldier;

    CookingMaterial cookieDone; //出すクッキー
    CookingMaterial setElem; //作ったクッキー

    //答えのクッキーの親オブジェクト
    public GameObject exit;

    //答えクッキー
    public CookingMaterial Answer
    {
        get; set;
    }

    void Start()
    {

    }

    void Update()
    {
        if (exit.transform.childCount >= 1)
        {
            //AnswerCookieはexitの子オブジェクト
            Answer = exit.transform.GetChild(0).GetComponent<CookingMaterial>();
        }
    }

    public CookingMaterial GetElement()
    {
        return null;
    }

    public void SetElement(CookingMaterial mat)
    {
        setElem = mat;

        if(setElem != null)
        {
            CookieRecipe();
        }
    }

    void CookieRecipe()
    {
        if(Answer.type == setElem.type)
        {
            float xRotate = -90.0f;

            cookieDone = setElem;

            CookingMaterial create = Instantiate(cookieDone, exit.transform.position, Quaternion.identity, exit.transform);
            create.gameObject.AddComponent<SoldierMove>();
            create.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            create.transform.eulerAngles = new Vector3(xRotate, 0, 0);

            Debug.Log("ok");
            setElem = null;
        }
    }
}