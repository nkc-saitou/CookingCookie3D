using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    public bool flg;
    public GameObject scene;

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        StartCoroutine(SceneFade());
    }

    IEnumerator SceneFade()
    {
        if (flg)
        {
            flg = false;
            Instantiate(scene);
            yield return new WaitForSeconds(0.5f);
            FadeManager.Instance.LoadScene("Result", 2.0f);
        }
    }

}
