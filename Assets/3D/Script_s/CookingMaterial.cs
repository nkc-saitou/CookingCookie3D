using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// aaaaaaaaaaaaaaaaaaaa
/// </summary>
public enum CookingMaterialType
{
    Dough = 0, //クッキーの生地
    Jam, //ジャム
    Choco, //チョコレート
    DarkMatter //ダークマター
}

public class CookingMaterial : MonoBehaviour {

    public CookingMaterialType type;
}