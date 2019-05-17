using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BakingTable : MonoBehaviour,IKitchenWare {

    //----------------------------------------------
    // public
    //----------------------------------------------

    //調理進行度を分かりやすくするための時計の針
    public GameObject clock;

    //出来たクッキーが何かを見せるための場所
    public GameObject createDisplayPos;

    //----------------------------------------------
    // private
    //----------------------------------------------

    //出来たクッキーが何かを見せるためのオブジェクト
    GameObject createDisplay;

    Animator anim;

    //出来たクッキーの
    CookieBaking createCookie;

    //調理進行度
    float checkProgress = 0;

    //調理をする時間
    float cookingTime = 2.0f;

    /// <summary>
    /// 入れられた素材、作ったクッキーを入れる用のリスト
    /// </summary>
    public List<CookingMaterial> elemLis
    {
        get; private set;
    }

    /// <summary>
    /// できたクッキーを入れる
    /// </summary>
    public CookingMaterial createDone
    {
        get; set;
    }

    void Start () {
        createCookie = transform.parent.GetComponent<CookieBaking>();
        elemLis = new List<CookingMaterial>();
        anim = GetComponent<Animator>();
    }

	void Update () {

        //クッキーが出来た状態以外だったら、表示用クッキーを削除
        if (checkProgress != 1) Destroy(createDisplay);
    }

    /// <summary>
    /// 調理進行度を返す処理　0～１で返す
    /// </summary>
    /// <returns></returns>
    public float CheckProgress()
    {
        return checkProgress;
    }

    /// <summary>
    /// 調理進行度を管理
    /// </summary>
    /// <returns></returns>
    IEnumerator Cooking()
    {
        float t = 0;

        CookingRecipe();
        anim.SetTrigger("OvenStart");

        //調理進行度
        while (checkProgress < 1)
        {
            yield return null;
            t += Time.deltaTime;

            checkProgress = Mathf.Min(t / cookingTime, 1.0f);

            //時計の針の角度を変える
            clock.transform.rotation = Quaternion.Euler(clock.transform.rotation.x, clock.transform.rotation.y, (-checkProgress) * 360);

        }
        //調理進行度が１になったら
        if (checkProgress == 1)
        {
            // 何が作れたのかを見せるため、プレイヤーが作ったクッキーを表示する
            GameObject display = createDone.gameObject;

            yield return new WaitForSeconds(0.3f);

            // 作ったクッキーを表示する
            createDisplay = Instantiate(display, createDisplayPos.transform.position,Quaternion.identity,createDisplayPos.transform);
            createDisplay.transform.rotation = Quaternion.Euler(-90, 0, 0);

            AudioManager.Instance.PlaySE("Oven");
        }
    }

    /// <summary>
    /// 何を作るかのレシピ判定
    /// </summary>
    void CookingRecipe()
    {
        CookingMaterial mat = createCookie.setCookie[0];

        switch (elemLis[0].type)
        {
            case CookingMaterialType.Knead_Dough:
                mat = createCookie.setCookie[0];
                break;

            case CookingMaterialType.Knead_Choco:
                mat = createCookie.setCookie[1];
                break;

            case CookingMaterialType.Knead_Jam:
                mat = createCookie.setCookie[2];
                break;

            case CookingMaterialType.Knead_DarkMatter:
                mat = createCookie.setCookie[3];
                break;
        }

        createDone = mat;
    }



    /// <summary>
    /// 入れた素材をリストに入れ、調理を始める
    /// </summary>
    /// <param name="mat">入れた素材</param>
    public void SetElement(CookingMaterial mat)
    {
        elemLis.Add(mat);

        if(elemLis.Count == 1)
        {
            StartCoroutine(Cooking());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public CookingMaterial GetElement()
    {
        if (checkProgress != 1) return null;

        elemLis.Clear();
        checkProgress = 0;
        Destroy(createDisplay);
        return createDone;
    }
}