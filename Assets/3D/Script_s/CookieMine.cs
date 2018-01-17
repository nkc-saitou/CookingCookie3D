using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieMine : MonoBehaviour {
    public GameObject Explosion;

	void Start () {
		
	}
	
	void Explode () {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            Invoke("Explode", 1f);
        }
    }
}
