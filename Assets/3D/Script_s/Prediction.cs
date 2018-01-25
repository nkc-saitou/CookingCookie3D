using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prediction : MonoBehaviour {
    private GameObject enemy;
    private GameObject obj;
    private GameObject parentObject;

    public GameObject Enemy
    {
        get { return enemy; }
        set { enemy = value; }
    }
    public GameObject ParentObject
    {
        get { return parentObject; }
        set { parentObject = value; }
    }
    void Start () {
        //Invoke("Spawn", 2);
	}
    void Update()
    {
        //不要になったら削除
        transform.localScale+=new Vector3(0f,0.003f,0f);
        transform.position -= new Vector3(0f, 0.003f, 0f);
        if (transform.localScale.y >= 1 &&!GameObject.Find("EnemySpawn").GetComponent<EnemySpawn>()._Clear)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        obj=(GameObject)Instantiate(enemy,new Vector3(transform.position.x,0,transform.position.z), Quaternion.identity);
        obj.transform.parent = parentObject.transform;
        Destroy(gameObject);
        
    }
}
