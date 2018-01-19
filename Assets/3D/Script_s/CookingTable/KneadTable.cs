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

    public CookingMaterial[] checkLis;

    //レシピ
    CookieKnead createCookie;

    //調理進行度
    float progress = 0;

    //素材を混ぜることができる数
    int requiredElem = 2;

    /// <summary>
    /// 入れた素材を判定するリスト
    /// </summary>
    public List<CookingMaterial> elemLis
    {
        get; private set;
    }

    public CookingMaterial createDone
    {
        get; set;
    }



    void Start()
    {
        //リストの初期化
        elemLis = new List<CookingMaterial>();
        createDone = new CookingMaterial();

        createCookie = transform.parent.GetComponent<CookieKnead>();

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
        createDone = null;

        //入れられた素材をJam,Choco,Doughの順に並び替える
        checkLis = elemLis.OrderBy(m => m.type.ToString().Length).ThenBy(m => m.type.ToString()).ToArray();

        //レシピを判定する

        //elemLis == {Jam,Dough} → ジャムクッキー
        //elemLis == {Choco,Dough} →チョコクッキー
        //elemLis == {Dough,Dough} →ノーマルクッキー

        foreach (CookingMaterial mat in createCookie.setCookie)
        {
            //レシピ通りの場合、2つめの要素は必ずDough
            if (checkLis[1].type == CookingMaterialType.Dough)
            {
                //作ったクッキーをelemLis[0]に保存
                if (mat.type == checkLis[0].type)
                {
                    createDone = mat;
                    break;
                }
            }
            else if(mat.type == CookingMaterialType.DarkMatter) //レシピと違う場合はダークマター
            {
                createDone = mat;
                break;
            }
        }
        elemLis.Clear();
        progress = 1;
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

        //Debug.Log(elemLis[0].gameObject);

        progress = 0;
        return createDone;
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
