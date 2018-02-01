using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSetting : SingletonMonoBehaviour<PlayerMoveSetting>
{
    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    //演出中か否か
    public bool MoveSettingFlg = true;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}
}
