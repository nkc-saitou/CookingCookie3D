using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour {

    //-----------------------------------------
    // public
    //-----------------------------------------
    [Header("クッキーの素")]
    public CookingMaterial doughPre;

    [Header("チョコレート")]
    public CookingMaterial chocolatePre;

    [Header("ジャム")]
    public CookingMaterial jamPre;

    [Header("こねたクッキー(ノーマル)")]
    public CookingMaterial kneadPre_normal;

    [Header("こねたクッキー(チョコレート)")]
    public CookingMaterial kneadPre_chocolate;

    [Header("こねたクッキー(ジャム)")]
    public CookingMaterial kneadPre_jam;

    [Header("こねたクッキー(ダークマター)")]
    public CookingMaterial kneadPre_darkMatter;

    [Header("焼いたクッキー(ノーマル)")]
    public CookingMaterial bakingPre_normal;

    [Header("焼いたクッキー(チョコレート)")]
    public CookingMaterial bakingPre_chocolate;

    [Header("焼いたクッキー(ジャム)")]
    public CookingMaterial bakingPre_jam;

    [Header("焼いたクッキー(ダークマター)")]
    public CookingMaterial bakingPre_darkMatter;

}
