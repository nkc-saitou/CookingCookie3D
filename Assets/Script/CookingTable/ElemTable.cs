using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElemTable : MonoBehaviour,IExecutable {

    //---------------------------------------------------
    // public
    //---------------------------------------------------

    //出す素材
    public CookingMaterial elemMat;

    public void SetElement(CookingMaterial mat) { }

    //クッキーの素材を出す
    public CookingMaterial GetElement()
    {
        return elemMat;
    }
}
