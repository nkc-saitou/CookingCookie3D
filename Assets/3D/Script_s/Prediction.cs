using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prediction : MonoBehaviour {

    private GameObject enemy;

    public GameObject Enemy
    {
        get { return enemy; }
        set { enemy = value; }
    }
	void Start () {
        //Invoke("Spawn", 2);
	}
    void Update()
    {
        //不要になったら削除する内容
        transform.localScale+=new Vector3(0f,0.003f,0f);
        transform.position -= new Vector3(0f, 0.003f, 0f);
        if (transform.localScale.y >= 1)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(enemy,transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
