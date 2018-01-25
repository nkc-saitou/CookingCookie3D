using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHP : MonoBehaviour {
    public int _hp=20;

    //0:東  1:南  2:北  3:西
    public int direction;

    public int _HP
    {
        get { return _hp; }
        set { _hp = value; }
    }

	void Start () {

    }
	
	void Update () {
        if (_hp <= 0)
        {
            Broken();
        }
		
	}

    void Broken()
    {
        GameObject.FindObjectOfType<EnemySpawn>().GetComponent<EnemySpawn>().wallBroken[direction] = true;

    }
}
