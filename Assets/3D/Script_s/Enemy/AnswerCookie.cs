using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerCookie : MonoBehaviour {

    public CookieSoldier cookieImage;

    public Image answer;
    Color answerColor;

	void Start ()
    {
        answerColor = answer.color;
	}

    void Update()
    {
        if (transform.childCount != 0)
        {
            //子オブジェクトの取得
            GameObject childCookie = transform.GetChild(0).gameObject;
            //子オブジェクトのtypeを取得する
            CookingMaterial cookieAnswer = childCookie.GetComponent<CookingMaterial>();

            if (cookieAnswer == null) return;

            switch(cookieAnswer.type)
            {
                case CookingMaterialType.Bake_Dough:

                    answer.sprite = cookieImage.answerImage[0];
                    break;

                case CookingMaterialType.Bake_Choco:

                    answer.sprite = cookieImage.answerImage[1];
                    break;

                case CookingMaterialType.Bake_Jam:

                    answer.sprite = cookieImage.answerImage[2];
                    break;
            }
            answerColor.a = 1;
        }
        else
        {
            answerColor.a = 0;
        }
        answer.color = answerColor;
    }
}