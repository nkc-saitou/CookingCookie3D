using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CookieDate : ScriptableObject {

    //----------------------------------------------------
    // private
    //----------------------------------------------------
    public string cookieKing;

    //----------------------------------------------------
    // 列挙型
    //----------------------------------------------------
    public enum CookieType
    {
        normalCookie = 0,
        jamCookie,
        chocolateCookie,
        darkMatter
    }

    public CookieType cookieType;

    public void TypeSet()
    {
        switch(cookieType)
        {
            case CookieType.normalCookie:
                cookieKing = "normalCookie";
                break;

            case CookieType.jamCookie:
                cookieKing = "jamCookie";
                break;

            case CookieType.chocolateCookie:
                cookieKing = "chocolateCookie";
                break;

            case CookieType.darkMatter:
                cookieKing = "darkMatter";
                break;
        }
    }

    //----------------------------------------------------
    // getter
    //----------------------------------------------------
    //public string CookieKing
    //{
    //    get { return cookieKing; }
    //}
}
