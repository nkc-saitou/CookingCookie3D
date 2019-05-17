using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeDamage : MonoBehaviour {
    void Start()
    {
        Invoke("Death", 0.5f);
    }

    void Death()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<EnemyMove>()._HP -= 100;
        }
    }
}
