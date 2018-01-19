using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

    IExecutable exe;
    IKitchenWare kit;

    TableKind table;

    PlayerSetting set_p;
    GameObject childObj;

    int childCount = 0;

    CookingMaterial childMat;

    CookingMaterial childType;

	// Use this for initialization
	void Start ()
    {
        table = new TableKind();
        childCount = transform.childCount;
        set_p = GetComponent<PlayerSetting>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        HaveCookieManager();

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

            childType = childObj.GetComponent<CookingMaterial>();
        }
        //子オブジェクトを持っていなかったら初期化
        else if (transform.childCount < childCount)
        {
            childObj = null;
            childType = null;
        }
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
                        SetGet_kit();
                        Destroy(childObj);

                    }
                    else if(knead.createDone != null && HaveChildObj() == false)
                    {
                        SetGet_kit();
                        knead.createDone = null;
                    }

                    break;

                case TableType.BakingTable:

                    BakingTable bake = new BakingTable();
                    bake = col.gameObject.GetComponent<BakingTable>();

                    if (KneadType() && bake.createDone == null && bake.elemLis.Count <= 0)
                    {
                        SetGet_kit();
                        Destroy(childObj);
                    }
                    else if (bake.createDone != null && HaveChildObj() == false)
                    {
                        SetGet_kit();
                        bake.createDone = null;
                    }

                    break;

                case TableType.ExitTable:
                    break;

                case TableType.Table:
                    break;
            }




            //if (exe != null && kitchenWare == null)
            //{
            //    SetGet_exe(exe);
            //}
            //if (kitchenWare != null)
            //{
            //    SetGet_kit(kitchenWare);
            //}
        }
            //exe = null;
            //kitchenWare = null;

    }

    bool ElemType()
    {
        if (childType.type == CookingMaterialType.Dough ||
            childType.type == CookingMaterialType.Jam ||
            childType.type == CookingMaterialType.Choco) return true;

        return false;
    }

    bool KneadType()
    {
        if (childType.type == CookingMaterialType.Knead_Dough ||
            childType.type == CookingMaterialType.Knead_Jam ||
            childType.type == CookingMaterialType.Knead_Choco ||
            childType.type == CookingMaterialType.Knead_DarkMatter) return true;

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
        else
        {

        }
    }

    void SetGet_kit()
    {
        if (HaveChildObj() == false)
        {
            childMat = kit.GetElement();
            Debug.Log(childMat.type);
            if (childMat != null) Instantiate(childMat.gameObject, transform);
        }
        else
        {
            if (childMat != null)
            {
                kit.SetElement(childMat);
                //Destroy(childObj);
                childMat = null;
            }
        }
    }

    void OnCollisionExit(Collision col)
    {
        //exe = null;
        //kitchenWare = null;
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

            // 親子関係を解除
            childObj.transform.parent = null;

            // 子オブジェクトを初期化
            childObj = null;
        }
    }
}