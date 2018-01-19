using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//===================================================
// 出口オブジェクトクラス
//===================================================
[System.Serializable]
public class DirectionName
{
    public string name = " ";
    
    [Header("工場内のオブジェクト")]
    public GameObject destroyObj;

    [Header("外のオブジェクト")]
    public GameObject createObj;

    public enum DirName
    {
        North = 0, //北
        South,     //南
        East,      //東
        West       //西
    }

    [Header("どの方向に置かれているのか")]
    public DirName dirName;
}

//===================================================

public class ExitTableController : MonoBehaviour
{
    [Header("ノーマルクッキー")]
    public GameObject normalSoldier; //作ったクッキーのPrefab
    [Header("ジャムクッキー")]
    public GameObject jamSoldier;
    [Header("チョコレートクッキー")]
    public GameObject chocolateSoldier;

    public DirectionName[] directionName;

    GameObject soldier;

    void Update()
    {
        foreach(DirectionName n in directionName)
        {
            //子オブジェクトがある状態だったら
            if (n.destroyObj.transform.childCount >= 1)
            {
                GameObject childObj = n.destroyObj.transform.GetChild(0).gameObject;
                CookieStatus status = childObj.GetComponent<CookieStatus>();

                switch (status.cookieDate.cookieKing)
                {
                    case "normalCookie":
                        soldier = normalSoldier;
                        break;

                    case "jamCookie":
                        soldier = jamSoldier;
                        break;

                    case "chocolateCookie":
                        soldier = chocolateSoldier;
                        break;
                }

                //外にクッキーを出す
                Instantiate(
                    soldier, 
                    directionName[(int)n.dirName].createObj.transform.localPosition,
                    Quaternion.identity,directionName[(int)n.dirName].createObj.transform);

                //中で作ったクッキーを削除
                Destroy(childObj);
            }
        }
    }
}