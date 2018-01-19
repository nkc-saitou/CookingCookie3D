using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingTable : MonoBehaviour,IKitchenWare {

    //----------------------------------------------
    // private
    //----------------------------------------------

    CookieBaking createCookie;

    //調理進行度
    float checkProgress = 0;
    
    //調理をする時間
    int cookingTime = 2;

    /// <summary>
    /// 入れられた素材、作ったクッキーを入れる用のリスト
    /// </summary>
    public List<CookingMaterial> elemLis
    {
        get; private set;
    }

    public CookingMaterial createDone
    {
        get; set;
    }

    // Use this for initialization
    void Start () {
        createCookie = transform.parent.GetComponent<CookieBaking>();
        elemLis = new List<CookingMaterial>();
        createDone = new CookingMaterial();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(checkProgress);
	}

    /// <summary>
    /// 調理進行度を管理
    /// </summary>
    /// <returns></returns>
    IEnumerator Cooking()
    {
        float t = 0;

        CookingRecipe();

        while (checkProgress <= 1)
        {
            yield return null;
            t += Time.deltaTime;
            checkProgress = t / cookingTime;
        }
        if (checkProgress >= 1.0f) checkProgress = 1.0f;
    }

    void CookingRecipe()
    {
        foreach (CookingMaterial mat in createCookie.setCookie)
        {
            switch(elemLis[0].type)
            {
                case CookingMaterialType.Knead_Dough:
                    if (mat.type == CookingMaterialType.Bake_Dough)
                        createDone = mat; 
                    break;

                case CookingMaterialType.Knead_Jam:
                    if (mat.type == CookingMaterialType.Bake_Jam)
                        createDone = mat;
                        break;

                case CookingMaterialType.Knead_Choco:
                    if (mat.type == CookingMaterialType.Bake_Choco)
                        createDone = mat;
                        break;

                case CookingMaterialType.Knead_DarkMatter:
                    if (mat.type == CookingMaterialType.Bake_DarkMatter)
                        createDone = mat;
                    break;
            }

        }
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
        if (CheckProgress() != 1) return null;
        elemLis.Clear();
        return createDone;
    }
}
