using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BakingTable : MonoBehaviour,IKitchenWare {

    //----------------------------------------------
    // private
    //----------------------------------------------

    public GameObject hari;
    public GameObject createDisplayPos;

    GameObject createDisplay;

    Animator anim;

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

    public CookingMaterial createDone
    {
        get; set;
    }

    // Use this for initialization
    void Start () {
        createCookie = transform.parent.GetComponent<CookieBaking>();
        elemLis = new List<CookingMaterial>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        //text.text = checkProgress.ToString();
        hari.transform.rotation = Quaternion.Euler(hari.transform.rotation.x, hari.transform.rotation.y, (-checkProgress) * 360);

        if (CheckProgress() != 1 && createDisplay != null) Destroy(createDisplay);
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
        while (checkProgress < 1)
        {
            yield return null;
            t += Time.deltaTime;

            checkProgress = Mathf.Min(t / cookingTime, 1.0f);
        }

        if (checkProgress == 1)
        {
            GameObject createDis = createDone.gameObject;

            yield return new WaitForSeconds(0.3f);
            createDisplay = Instantiate(createDis, createDisplayPos.transform.position,Quaternion.identity,createDisplayPos.transform);
            createDisplay.transform.rotation = Quaternion.Euler(-90, 0, 0);
            AudioManager.Instance.PlaySE("Oven");
        }
    }

    /// <summary>
    /// 何を作るかの判定
    /// </summary>
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
        checkProgress = 0;
        Destroy(createDisplay);
        return createDone;
    }
}