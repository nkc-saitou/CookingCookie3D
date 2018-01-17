using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionStay(Collision col)
    {
        IExecutable exe = col.gameObject.GetComponent(typeof(IExecutable)) as IExecutable;
        IKitchenWare kitchenWare = col.gameObject.GetComponent(typeof(IKitchenWare)) as IKitchenWare;

        if (exe == null && kitchenWare == null) return;

        if (exe != null)
        {

        }
        if(kitchenWare != null)
        {

        }
    }

    void OnCollisionExit(Collision col)
    {
        
    }
}