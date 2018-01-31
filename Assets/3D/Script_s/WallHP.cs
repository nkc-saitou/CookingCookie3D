using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallHP : MonoBehaviour {

    public float MaxHp=20;
    public float _hp=20;
    public Image back;
    public Image front;

    //0:東  1:南  2:北  3:西
    [Header("0:東  1:南  2:北  3:西")]
    public int direction;

    public float _HP
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (breakFlg == false)
            {
                front.fillAmount = _hp / MaxHp;
            }
        }
    }

    public bool breakFlg
    {
        get; set;
    }

	void Start () {
        breakFlg = false;
    }
	
	void Update () {

        if (_hp <= 0)
        {
            Broken();
            Destroy(back);
            Destroy(front);
        }
        if(_hp > 0)
        {
            breakFlg = false;
        }
	}

    void Broken()
    {
        GameObject.FindObjectOfType<EnemySpawn>().GetComponent<EnemySpawn>().wallBroken[direction] = true;
        breakFlg = true;
    }
}