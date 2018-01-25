using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ScoreManeger : MonoBehaviour {
    private float score=0;
    private float highscore;
    private List<float> scoreList= new List<float> { 0,0,0,0,0,0,0,0,0,0,0};
    private string[] scorekey=new string[10];
    public Text tx;

    public float Score
    {
        get { return score; }
        set
        {
            score = value;
            if (score >= highscore) { highscore = score; }
        }
    }

    public float HighScore
    {
        get { return highscore; }
        set { score = value;    }
    }

	void Start () {
        Load();
        highscore = scoreList.Max();
    }
	

	void Update () {
        tx.text ="High Score  "+ highscore.ToString()+ "\nScore           "+score.ToString();
    }

    public void AddScore()
    {
        scoreList.Add(score);
        scoreList.ToArray();
        scoreList.Reverse();
        scoreList.RemoveAt(10);
        for (int i = 0; i < 10; i++)
        {
            scorekey[i] = "scorekey" + i;
            PlayerPrefs.SetFloat(scorekey[i], scoreList[i]);
        }
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }

    public void Load()
    {
        for (int j = 0; j < 10; j++)
        {
            scorekey[j] = "scorekey" + j;
            scoreList[j]=PlayerPrefs.GetFloat(scorekey[j], 0);
        }
    }
}
