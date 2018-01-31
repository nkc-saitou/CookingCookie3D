using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberofEnemy : MonoBehaviour {
    public Sprite[] numimage;
    public List<int> number = new List<int>();
    private int numberEnemy = 0;
    private Image image;

    public int NumberEnemy
    {
        get { return numberEnemy; }
        set { numberEnemy = value;
            View();
        }
    }
    void Start () {
        image = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void View () {
        image.sprite = numimage[numberEnemy];
    }
}
