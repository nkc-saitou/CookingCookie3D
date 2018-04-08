using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTable : MonoBehaviour,IExecutable
{
    //出すクッキー
    CookingMaterial cookieDone;

    //作ったクッキー
    CookingMaterial setElem; 

    //壁のHP
    public WallHP wallHP;

    //クッキーが違う時の案内表示
    public GameObject missImg;

    //答えのクッキーの親オブジェクト
    public GameObject exit;

    /// <summary>
    /// 答えクッキー
    /// </summary>
    public CookingMaterial Answer
    {
        get; set;
    }

    void Start()
    {
        missImg.SetActive(false);
    }

    void Update()
    {
        if (exit.transform.childCount >= 1)
        {
            //AnswerCookieはexitの子オブジェクト
            Answer = exit.transform.GetChild(0).GetComponent<CookingMaterial>();
        }

        //表示時間
        if (missImg.activeSelf) StartCoroutine(DisplayTime());
    }

    /// <summary>
    /// ゲット
    /// </summary>
    /// <returns></returns>
    public CookingMaterial GetElement()
    {
        return null;
    }

    /// <summary>
    /// 素材を置く
    /// </summary>
    /// <param name="mat"></param>
    public void SetElement(CookingMaterial mat)
    {
        setElem = mat;

        if(setElem != null)
        {
            CookieRecipe();
        }
    }

    /// <summary>
    /// 置こうとしているクッキーと、出さないといけないクッキーが同じかどうかを判定
    /// </summary>
    void CookieRecipe()
    {
        if (Answer.type == setElem.type && exit.transform.childCount != 0)
        {

            //クッキーを出す
            cookieDone = setElem;
            CookingMaterial create = Instantiate(cookieDone, exit.transform.position, Quaternion.identity, exit.transform);

            create.gameObject.AddComponent<SoldierMove>();
            create.gameObject.GetComponent<BoxCollider>().isTrigger = false;

            //出したクッキーの角度を調整
            float xRotate = -90.0f;
            create.transform.eulerAngles = new Vector3(xRotate, 0, 0);

            setElem = null;
        }
    }

    /// <summary>
    /// 表示時間を設定
    /// </summary>
    /// <returns></returns>
    IEnumerator DisplayTime()
    {
        //1秒後に非表示
        yield return new WaitForSeconds(1.0f);
        missImg.SetActive(false);
    }
}