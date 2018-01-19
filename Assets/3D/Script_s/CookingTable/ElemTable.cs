using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElemTable : MonoBehaviour,IExecutable {

    //---------------------------------------------------
    // private
    //---------------------------------------------------

    //---------------------------------------------------
    // public
    //---------------------------------------------------

    public CookingMaterial elemMat;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetElement(CookingMaterial mat)
    {

    }

    public CookingMaterial GetElement()
    {
        return elemMat;
    }
}
