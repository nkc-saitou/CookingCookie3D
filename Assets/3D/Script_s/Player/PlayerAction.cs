using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour {

    //-------------------------------------------
    // public
    //-------------------------------------------

    //素材を持つ位置
    public Transform havePos;

    //-------------------------------------------
    // private
    //-------------------------------------------

    //衝突した机のinterfaceを取得するための変数
    IExecutable exe;
    IKitchenWare kit;

    //衝突した机にアタッチされているスプリクトを取得
    TableKind table;
    KneadTable knead;
    BakingTable bake;

    PlayerSetting setting;

    //もっている素材
    GameObject haveObj;

    //持っているオブジェクトの数
    int haveObjCount = 0;

    //素材の種類を取得
    CookingMaterial haveObjMat;

	void Start ()
    {
        //子オブジェクトの数
        haveObjCount = transform.childCount;
        setting = GetComponent<PlayerSetting>();
	}

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
        if (transform.childCount < haveObjCount + 1) return false;
        else return true;
    }

    /// <summary>
    /// 子オブジェクトの管理と初期化
    /// </summary>
    void HaveCookieManager()
    {
        //子オブジェクトの特定
        if (transform.childCount >= haveObjCount + 1)
        {
            haveObj = transform.GetChild(haveObjCount).gameObject;

            haveObjMat = haveObj.GetComponent<CookingMaterial>();

            haveObj.transform.localPosition = havePos.localPosition;
        }
    }

    void OnCollisionStay(Collision col)
    {
        //各コントローラーのAction1ボタンが押されたら
        if (Input.GetButtonDown(setting.keyAction))
        {
            //衝突した机の種類を取得、tableKindがついていないオブジェクトだったら処理を中断
            table = col.gameObject.GetComponent(typeof(TableKind)) as TableKind;
            if (table == null) return;

            //衝突した机のinterfaceを取得する
            exe = col.gameObject.GetComponent(typeof(IExecutable)) as IExecutable;
            kit = col.gameObject.GetComponent(typeof(IKitchenWare)) as IKitchenWare;

            switch (table.type)
            {
                //素材テーブル
                case TableType.ElemTable:

                    SetGet_exe();
                    break;

                //こねるテーブル
                case TableType.KneadTable:

                    knead = col.gameObject.GetComponent<KneadTable>();

                    //入れる
                    if (knead.elemLis.Count < 2 && ElemType() && knead.createDone == null)
                    {
                        KneadSetGet_kit();
                        Destroy(haveObj);
                    }
                    //出す
                    else if (knead.createDone != null && HaveChildObj() == false && knead.CheckProgress() == 1)
                    {
                        KneadSetGet_kit();
                        knead.createDone = null;
                    }

                    break;

                //オーブン
                case TableType.BakingTable:

                    bake = col.gameObject.GetComponent<BakingTable>();

                    //入れる
                    if (KneadType() && bake.elemLis.Count <= 0 && bake.createDone == null)
                    {
                        OvenSetGet_kit();
                        Destroy(haveObj);
                    }
                    //出す
                    else if (bake.createDone != null && HaveChildObj() == false && bake.CheckProgress() == 1)
                    {
                        OvenSetGet_kit();
                        bake.createDone = null;
                    }

                    break;

                //コンベア
                case TableType.ExitTable:

                    ExitTable exit = col.gameObject.GetComponent<ExitTable>();

                    //入れる
                    if (HaveChildObj() && BakingType(exit))
                    {
                        exe.SetElement(haveObjMat);
                        haveObjMat = null;
                        Destroy(haveObj);
                    }
                    //出す
                    else if (haveObjMat != null && BakingType(exit) == false)
                    {
                        exit.missImg.SetActive(true);
                    }

                    break;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        FloorUp(col.gameObject);
    }

    void OnCollisionExit(Collision col)
    {
        //各変数の初期化
        table = null;
        exe = null;
        kit = null;
    }

    /// <summary>
    /// 入れられた素材がテーブルに入れられる素材かどうかを判断
    /// </summary>
    /// <returns></returns>
    bool ElemType()
    {
        if (haveObjMat == null) return false;

        if (haveObjMat.type == CookingMaterialType.Dough ||
            haveObjMat.type == CookingMaterialType.Jam ||
            haveObjMat.type == CookingMaterialType.Choco) return true;

        return false;
    }

    /// <summary>
    /// 入れられた素材がテーブルに入れられる素材かどうかを判断
    /// </summary>
    /// <returns></returns>
    bool KneadType()
    {
        if (haveObjMat == null) return false;

        if (haveObjMat.type == CookingMaterialType.Knead_Dough ||
            haveObjMat.type == CookingMaterialType.Knead_Jam ||
            haveObjMat.type == CookingMaterialType.Knead_Choco ||
            haveObjMat.type == CookingMaterialType.Knead_DarkMatter) return true;

        return false;
    }

    /// <summary>
    /// 入れられた素材が出せるクッキーかどうかを判断
    /// </summary>
    /// <returns></returns>
    bool BakingType(ExitTable exit)
    {
        if (exit.Answer == null || haveObjMat == null || exit.exit.transform.childCount == 0) return false;
        if (exit.Answer.type == haveObjMat.type) return true;

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
            haveObjMat = exe.GetElement();
            Instantiate(haveObjMat.gameObject, transform);
        }
    }

    /// <summary>
    /// オーブン素材のゲットセット
    /// </summary>
    /// <param name="exe"></param>
    void OvenSetGet_kit()
    {
        //ゲット
        if (HaveChildObj() == false)
        {
            haveObjMat = kit.GetElement();
            if (haveObjMat != null)
            {
                Instantiate(haveObjMat.gameObject, transform);
                bake.createDone = null;
            }
        }
        //セット
        else if (haveObjMat != null)
        {
            kit.SetElement(haveObjMat);
            haveObjMat = null;
        }
    }

    /// <summary>
    /// こねる机の素材のゲットセット
    /// </summary>
    /// <param name="exe"></param>
    void KneadSetGet_kit()
    {
        //ゲット
        if (HaveChildObj() == false)
        {
            haveObjMat = kit.GetElement();
            if (haveObjMat != null)
            {
                Instantiate(haveObjMat.gameObject, transform);
            }
        }
        //セット
        else if (haveObjMat != null)
        {
            kit.SetElement(haveObjMat);
            haveObjMat = null;
        }
    }

    /// <summary>
    /// 床に置くときの処理
    /// </summary>
    void FloorPut()
    {
        // クッキーを持っていなかったら終了
        if (HaveChildObj() == false) return;

        if (Input.GetButtonUp(setting.keyAction_2) && haveObj != null)
        {
            haveObj.transform.position = gameObject.transform.position;
            Vector3 pos = haveObj.transform.position;
            pos.y += 0.3f;
            haveObj.transform.position = pos;

            // 親子関係を解除
            haveObj.transform.parent = null;

            // 子オブジェクトを初期化
            haveObj = null;
        }
    }

    /// <summary>
    /// 床に落ちているものを拾う処理
    /// </summary>
    /// <param name="col">衝突した物</param>
    void FloorUp(GameObject col)
    {
        //オブジェクトを持っていなかったら処理を中断
        if (HaveChildObj()) return;

        //落ちてるものが素材かどうかを判断する
        CookingMaterial floorMat;
        floorMat = col.GetComponent(typeof(CookingMaterial)) as CookingMaterial;

        if(Input.GetButtonUp(setting.keyAction) && floorMat != null)
        {
            col.transform.parent = transform;
        }
    }
}