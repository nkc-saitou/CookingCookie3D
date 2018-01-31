using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneBGM : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioManager.Instance.PlayBGM("game");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
