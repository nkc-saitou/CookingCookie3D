using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

    IExecutable exe;
    IKitchenWare kitchenWare;

    PlayerSetting set_p;
    GameObject childObj;

    int childCount = 0;

    CookingMaterial childMat;


    public CookingMaterial doss;

	// Use this for initialization
	void Start ()
    {
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
        }
        //子オブジェクトを持っていなかったら初期化
        else if (transform.childCount < childCount)
        {
            childObj = null;
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (Input.GetButtonDown(set_p.keyAction))
        {
            exe = col.gameObject.GetComponent(typeof(IExecutable)) as IExecutable;
            kitchenWare = col.gameObject.GetComponent(typeof(IKitchenWare)) as IKitchenWare;

            if (exe != null && kitchenWare == null)
            {
                SetGet_exe(exe);
            }
            if (kitchenWare != null)
            {
                SetGet_kit(kitchenWare);
            }
        }
            exe = null;
            kitchenWare = null;
    }

    /// <summary>
    /// 素材のゲットセット
    /// </summary>
    /// <param name="exe"></param>
    void SetGet_exe(IExecutable exe)
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

    void SetGet_kit(IKitchenWare kit)
    {
        if (HaveChildObj() == false)
        {
            childMat = kit.GetElement();
            if (childMat != null) Instantiate(childMat.gameObject, transform);
        }
        else
        {
            if (childMat != null)
            {
                kit.SetElement(childMat);
                Destroy(childObj);
                childMat = null;
            }
        }
    }

    void OnCollisionExit(Collision col)
    {
        exe = null;
        kitchenWare = null;
    }
}