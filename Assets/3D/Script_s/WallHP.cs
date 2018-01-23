using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHP : MonoBehaviour {
    public int _hp=100;

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
        ScoreManeger SM;
        SM=GameObject.FindObjectOfType<ScoreManeger>().GetComponent<ScoreManeger>();
        SM.AddScore();
        SM.Save();
        Destroy(gameObject);
    }
}
