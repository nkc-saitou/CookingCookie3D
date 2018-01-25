using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// aaaaaaaaaaaaaaaaaaaa
/// </summary>
public enum CookingMaterialType
{
    //====素材====
    Dough = 0, //クッキーの生地
    Jam, //ジャム
    Choco, //チョコ
    //====こねた素材====
    Knead_Dough, //ノーマル
    Knead_Jam, //ジャム
    Knead_Choco, //チョコ
    Knead_DarkMatter, //失敗作(だーくまたー)
    //====焼いた素材====
    Bake_Dough, //ノーマル
    Bake_Jam, //ジャム
    Bake_Choco, //チョコ
    Bake_DarkMatter //失敗作(だーくまたー)
}

public class CookingMaterial : MonoBehaviour {

    public CookingMaterialType type;
}