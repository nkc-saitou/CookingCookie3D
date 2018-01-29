using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class KneadTable : MonoBehaviour, IKitchenWare
{
    //-----------------------------------------------
    // private
    //-----------------------------------------------

    CookingMaterial[] checkLis;

    //レシピ
    CookieKnead createCookie;

    //表示用のクッキーを出す位置
    [Header("１つめの素材、２つめの素材、出来たクッキー")]
    public Transform[] displayPos;

    public GameObject effectPre;

    //調理進行度
    float progress = 0;

    //調理をする時間
    float cookingTime = 1.0f;

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

        createCookie = transform.parent.GetComponent<CookieKnead>();

        //継続的に代入できればいい
        //float a = StartCoroutine(Cooking());
    }

    void Update()
    {

    }

    /// <summary>
    /// 調理進行度を管理
    /// </summary>
    /// <returns></returns>
    IEnumerator Cooking()
    {
        float t = 0;

        CookingRecipe();

        while (progress < 1)
        {
            yield return null;
            t += Time.deltaTime;
            progress = Mathf.Min(t / cookingTime, 1.0f);
        }
        //if (checkProgress >= 1.0f) checkProgress = 1.0f;
    }

    /// <summary>
    /// 素材がレシピ通りかどうかを判定
    /// </summary>
    void CookingRecipe()
    {
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
                if (mat.type.ToString() == "Knead_" + checkLis[0].type.ToString())
                {
                    createDone = mat;
                    Debug.Log(createDone);
                    DisplayCookie(createDone);
                    break;
                }
            }
            else if (mat.type == CookingMaterialType.Knead_DarkMatter) //レシピと違う場合はダークマター
            {
                createDone = mat;
                DisplayCookie(mat);
                break;
            }
        }
        //elemLis.Clear();
    }

    /// <summary>
    /// 机に素材を置く
    /// </summary>
    /// <param name="mat">素材の種類</param>
    public void SetElement(CookingMaterial mat)
    {
        elemLis.Add(mat);

        DisplayElem(mat);

        if (elemLis.Count == 2)
        {
            //調理をはじめる
            StartCoroutine(Cooking());
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
        DisplayDestroy();
        elemLis.Clear();
        progress = 0;
        //DisplayDestroy();
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

    /// <summary>
    /// テーブルに置いたクッキーを見えるようにする
    /// </summary>
    /// <param name="mat">素材のtype</param>
    void DisplayElem(CookingMaterial mat)
    {
        //置かれた素材を机の上に配置
        GameObject display = Instantiate(
            mat.gameObject,
            displayPos[elemLis.Count - 1].transform.position,
            Quaternion.identity,
            displayPos[elemLis.Count - 1].transform);

        //チョコ以外位置調整
        if (mat.type != CookingMaterialType.Choco) display.transform.eulerAngles = new Vector3(-90, 0, 0);

    }

    /// <summary>
    /// 作ったクッキーを机の上に置く
    /// </summary>
    /// <param name="mat">作ったクッキーのtype</param>
    void DisplayCookie(CookingMaterial mat)
    {

        for (int i = 0; i < displayPos.Length - 1; i++)
        {
            Destroy(displayPos[i].gameObject.transform.GetChild(0).gameObject);
        }

        GameObject effect = Instantiate(effectPre);

        Destroy(effect, 1.0f);

        GameObject displayCookie = Instantiate(
                mat.gameObject,
                displayPos[2].transform.position,
                Quaternion.identity,
                displayPos[2].transform);

        displayCookie.transform.eulerAngles = new Vector3(-90, 0, 0);

    }

    /// <summary>
    /// Display用に作ったクッキーを机の上から消す
    /// </summary>
    void DisplayDestroy()
    {
        Destroy(displayPos[2].transform.GetChild(0).gameObject);
    }
}
