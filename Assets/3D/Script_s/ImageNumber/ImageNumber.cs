using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NumberType
{
    enemyCount = 0,
    score,
    highScore
}

public class ImageNumber : MonoBehaviour {

    public Image[] numSp;

    public NumberType numberType;

    public Sprite[] scoreNum;

    public static bool _EnemyCountFlg = false;

    int score;
    int highScore;

    Color color; //a値１
    Color color_a; //a値０

    public List<int> imageLis = new List<int>();

    int imageNum;

	// Use this for initialization
	void Start () {

        DecisionType();
        ChangeImage();

        color.a = 1;
        color_a.a = 0;

        score = ScoreManagerS.Instance.Score;
        highScore = ScoreManagerS.Instance.HighScore;
    }
	
	// Update is called once per frame
	void Update () {
        if(numberType == NumberType.enemyCount) DecisionType();
    }

    void DecisionType()
    {
        switch(numberType)
        {
            case NumberType.enemyCount:
                imageNum = EnemyDeath._EnemyDeath;
 
                if(_EnemyCountFlg)
                {
                    _EnemyCountFlg = false;
                    ChangeImage();

                    if(imageLis.Count == 1) numSp[1].sprite = scoreNum[0];
                    
                }
                break;

            case NumberType.score:
                imageNum = ScoreManagerS.Instance.Score;
                ChangeImage();
                break;

            case NumberType.highScore:
                imageNum = ScoreManagerS.Instance.HighScore;
                ChangeImage();
                break;
        }
    }

    void ChangeImage()
    {
        imageLis.Clear();

        int digit = imageNum;

        while (digit != 0)
        {
            imageNum = digit % 10;
            digit = digit / 10;
            imageLis.Add(imageNum);
        }

        for (int i = 0; i < imageLis.Count; i++)
        {
            numSp[i].sprite = scoreNum[imageLis[i]];
        }

        if (imageLis.Count == 0)
        {
            numSp[0].sprite = scoreNum[0];
        }

        ScoreManagerS.Instance.ScoreRenew();
    }
}
