using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerCookie : MonoBehaviour {

    //答えクッキーのイメージ
    public CookieSoldier cookieImage;

    //答えクッキーを表示するための画像
    public Image answer;

    //表示と非表示を切り替えるための変数
    Color answerColor;

	void Start ()
    {
        answerColor = answer.color;
	}

    void Update()
    {
        //敵クッキーが湧いていたら
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
        //湧いていなかったら
        else
        {
            answerColor.a = 0;
        }
        answer.color = answerColor;
    }
}