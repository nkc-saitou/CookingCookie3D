using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KneadTable : MonoBehaviour, IKitchenWare
{
    //-----------------------------------------------
    // private
    //-----------------------------------------------

    //レシピ
    CookingMaterial createCookie = new CookingMaterial();

    //調理進行度
    float progress = 1;

    //素材を混ぜることができる数
    int requiredElem = 2;

    /// <summary>
    /// 入れた素材を判定するリスト
    /// </summary>
    public List<CookingMaterial> elemLis
    {
        get; private set;
    }

    void Start()
    {
        elemLis = new List<CookingMaterial>();

        //継続的に代入できればいい
        //float a = StartCoroutine(Cooking());
    }

    void Update()
    {

    }

    /// <summary>
    /// 素材がレシピ通りかどうかを判定
    /// </summary>
    void CookingRecipe()
    {
        //入れられた素材をJam,Choco,Doughの順に並び替える
        CookingMaterial[] checkLis = elemLis.OrderBy(m => m.type.ToString().Length).ToArray();

        //レシピを判定する

        //elemLis == {Jam,Dough} → ジャムクッキー
        //elemLis == {Choco,Dough} →チョコクッキー
        //elemLis == {Dough,Dough} →ノーマルクッキー

        //レシピ通りの場合、2つめの要素は必ずDough
        if (elemLis[1].type == CookingMaterialType.Dough)
        {
            //リストの要素を全削除
            elemLis.Clear();

            //１つ目に入れられた素材でどのレシピかを判別し、クッキーを作る
            createCookie.type = elemLis[0].type;

            //作ったクッキーをelemLis[0]に保存
            elemLis.Add(createCookie);
        }
        else //レシピと違う場合はダークマター
        {
            elemLis.Clear();
            createCookie.type = CookingMaterialType.DarkMatter;
            elemLis.Add(createCookie);
        }
    }

    /// <summary>
    /// 机に素材を置く
    /// </summary>
    /// <param name="mat">素材の種類</param>
    public void SetElement(CookingMaterial mat)
    {
        elemLis.Add(mat);

        if (elemLis.Count == 2)
        {
            //調理をはじめる
            CookingRecipe();
        }
    }

    /// <summary>
    /// 出来たクッキーを返す処理
    /// </summary>
    /// <returns>出来たクッキー</returns>
    public CookingMaterial GetElement()
    {
        if (CheckProgress() != 1) return null;
        return elemLis[0];
    }

    /// <summary>
    /// 進行度を調べる処理
    /// </summary>
    /// <returns></returns>
    public float CheckProgress()
    {
        return progress;
    }
}
