using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour {

    //-------------------------------------------
    // public
    //-------------------------------------------
    public Transform childPos;

    //-------------------------------------------
    // private
    //-------------------------------------------
    IExecutable exe;
    IKitchenWare kit;

    TableKind table;
    BakingTable bake;

    PlayerSetting set_p;

    GameObject childObj;

    int childCount = 0;

    CookingMaterial childMat;

	// Use this for initialization
	void Start ()
    {
        table = new TableKind();
        bake = new BakingTable();

        childCount = transform.childCount;
        set_p = GetComponent<PlayerSetting>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        HaveCookieManager();
        FloorPut();
    }

    /// <summary>
    /// プレイヤーが子オブジェクトを持っているかどうか。
    /// </summary>
    /// <returns>持っていたらtreu,いなかったらfalse</returns>
    bool HaveChildObj()
    {
        if (transform.childCount < childCount + 1) return false;
        else return true;
    }

    /// <summary>
    /// 子オブジェクトの管理と初期化
    /// </summary>
    void HaveCookieManager()
    {
        //子オブジェクトの特定
        if (transform.childCount >= childCount + 1)
        {
            childObj = transform.GetChild(childCount).gameObject;

            childMat = childObj.GetComponent<CookingMaterial>();

            childObj.transform.localPosition = childPos.localPosition;
        }
        ////子オブジェクトを持っていなかったら初期化
        //else if (transform.childCount < childCount)
        //{
        //    childObj = null;
        //    childMat = null;
        //    //test.text = null;
        //}
    }

    void OnCollisionStay(Collision col)
    {
        if (Input.GetButtonDown(set_p.keyAction))
        {
            table = col.gameObject.GetComponent(typeof(TableKind)) as TableKind;
            if (table == null) return;

            exe = col.gameObject.GetComponent(typeof(IExecutable)) as IExecutable;
            kit = col.gameObject.GetComponent(typeof(IKitchenWare)) as IKitchenWare;

            switch (table.type)
            {
                case TableType.ElemTable:

                    SetGet_exe();
                    break;

                case TableType.KneadTable:

                    KneadTable knead = new KneadTable();
                    knead = col.gameObject.GetComponent<KneadTable>();

                    if (knead.elemLis.Count <= 1 && ElemType() && knead.createDone == null)
                    {
                        KneadSetGet_kit();
                        Destroy(childObj);

                    }
                    else if (knead.createDone != null && HaveChildObj() == false && knead.CheckProgress() == 1)
                    {
                        KneadSetGet_kit();
                        knead.createDone = null;
                    }

                    break;

                case TableType.BakingTable:

                    BakingTable bake = col.gameObject.GetComponent<BakingTable>();

                    Debug.Log(bake.elemLis.Count);

                    //入れる
                    if (KneadType() && bake.elemLis.Count <= 0 && HaveChildObj() && bake.createDone == null)
                    {
                        OvenSetGet_kit();
                        Destroy(childObj);
                    }
                    //出す
                    else if (bake.createDone != null && HaveChildObj() == false && bake.CheckProgress() == 1)
                    {
                        OvenSetGet_kit();
                        bake.createDone = null;
                    }

                    break;

                case TableType.ExitTable:
                    break;

                case TableType.Table:
                    break;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        FloorUp(col.gameObject);
    }

    /// <summary>
    /// 入れられた素材がテーブルに入れられる素材かどうかを判断
    /// </summary>
    /// <returns></returns>
    bool ElemType()
    {
        if (childMat == null) return false;

        if (childMat.type == CookingMaterialType.Dough ||
            childMat.type == CookingMaterialType.Jam ||
            childMat.type == CookingMaterialType.Choco) return true;

        return false;
    }

    /// <summary>
    /// 入れられた素材がテーブルに入れられる素材かどうかを判断
    /// </summary>
    /// <returns></returns>
    bool KneadType()
    {
        if (childMat == null) return false;

        if (childMat.type == CookingMaterialType.Knead_Dough ||
            childMat.type == CookingMaterialType.Knead_Jam ||
            childMat.type == CookingMaterialType.Knead_Choco ||
            childMat.type == CookingMaterialType.Knead_DarkMatter) return true;

        return false;
    }

    /// <summary>
    /// 素材のゲットセット
    /// </summary>
    /// <param name="exe"></param>
    void SetGet_exe()
    {
        if (HaveChildObj() == false)
        {
            childMat = exe.GetElement();
            Instantiate(childMat.gameObject, transform);
        }
    }

    void OvenSetGet_kit()
    {
        if (HaveChildObj() == false)
        {
            childMat = kit.GetElement();
            if (childMat != null)
            {
                Instantiate(childMat.gameObject, transform);
                bake.createDone = null;
                Debug.Log(bake.createDone);
            }
        }
        else
        {
            if (childMat != null)
            {
                kit.SetElement(childMat);
                childMat = null;
            }
        }
    }

    void KneadSetGet_kit()
    {
        if (HaveChildObj() == false)
        {
            childMat = kit.GetElement();
            if (childMat != null)
            {
                Instantiate(childMat.gameObject, transform);
            }
        }
        else
        {
            if (childMat != null)
            {
                kit.SetElement(childMat);
                childMat = null;
            }
        }
    }

    void OnCollisionExit(Collision col)
    {
        table = null;
        exe = null;
        kit = null;
    }

    /// <summary>
    /// 床に置くときの処理
    /// </summary>
    void FloorPut()
    {
        // クッキーを持っていなかったら終了
        if (HaveChildObj() == false) return;

        if (Input.GetButtonUp(set_p.keyAction_2) && childObj != null)
        {
            childObj.transform.position = gameObject.transform.position;
            Vector3 pos = childPos.transform.position;
            pos.y += 0.3f;
            childObj.transform.position = pos;

            // 親子関係を解除
            childObj.transform.parent = null;

            // 子オブジェクトを初期化
            childObj = null;
        }
    }

    /// <summary>
    /// 床に落ちているものを拾う処理
    /// </summary>
    /// <param name="col">衝突した物</param>
    void FloorUp(GameObject col)
    {
        CookingMaterial floorMat;
        floorMat = new CookingMaterial();

        floorMat = col.GetComponent(typeof(CookingMaterial)) as CookingMaterial;

        if(Input.GetButtonUp(set_p.keyAction) && floorMat != null)
        {
            col.transform.parent = transform;
        }
    }
}