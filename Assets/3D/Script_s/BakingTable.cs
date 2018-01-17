using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingTable : MonoBehaviour,IKitchenWare {

    public CookingMaterial mat;

    //----------------------------------------------
    // private
    //----------------------------------------------

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 調理進行度を管理
    /// </summary>
    /// <returns></returns>
    IEnumerator Cooking()
    {
        float t = 0;

        while(checkProgress <= 1)
        {
            yield return null;
            t += Time.deltaTime;
            checkProgress = t / cookingTime;
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
        return elemLis[0];
    }
}
