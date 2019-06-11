using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ScoreManeger : MonoBehaviour {
    private int _score = 0;
    private int highscore;
    private List<int> scoreList = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private string[] scorekey = new string[10];
    public Sprite[] numimage;
    public List<int> number = new List<int>();

    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            if (_score >= highscore) { highscore = _score; }
            var objs = GameObject.FindGameObjectsWithTag("Score");
            foreach (var obj in objs)
            {
                if (0 <= obj.name.LastIndexOf("Clone"))
                {
                    Destroy(obj);
                }
            }
            View(_score);
        }
    }

    public int HighScore
    {
        get { return highscore; }
        set { highscore = value; }
    }

    //スコアを表示する
    void View(int score)
    {
        var digit = score;
        //要素数0には１桁目の値が格納
        number = new List<int>();
        while (digit != 0)
        {
            score = digit % 10;
            digit = digit / 10;
            number.Add(score);
        }

        GameObject.Find("ScoreImage").GetComponent<Image>().sprite = numimage[number[0]];
        for (int i = 1; i < number.Count; i++)
        {
            //複製
            RectTransform scoreimage = (RectTransform)Instantiate(GameObject.Find("ScoreImage")).transform;
            scoreimage.SetParent(this.transform, false);
            scoreimage.localPosition = new Vector2(
                scoreimage.localPosition.x - scoreimage.sizeDelta.x * i / 4,
                scoreimage.localPosition.y);
            scoreimage.GetComponent<Image>().sprite = numimage[number[i]];
        }
    }

    void Start()
    {
        Load();
        highscore = scoreList.Max();
    }


    void Update()
    {
    }

    public void AddScore()
    {
        scoreList.Add(_score);
        scoreList.ToArray();
        scoreList.Reverse();
        scoreList.RemoveAt(10);
        for (int i = 0; i < 10; i++)
        {
            scorekey[i] = "scorekey" + i;
            PlayerPrefs.SetInt(scorekey[i], scoreList[i]);
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
            scoreList[j] = PlayerPrefs.GetInt(scorekey[j], 0);
        }
    }
}
