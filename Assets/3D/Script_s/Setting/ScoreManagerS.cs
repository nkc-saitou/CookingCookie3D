using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerS: SingletonMonoBehaviour<ScoreManagerS> {

    private int m_score;
    private int m_high_score;


    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// スコア
    /// </summary>
    public int Score
    {
        set
        {
            m_score = value;
        }
        get
        {
            return m_score;
        }
    }

    /// <summary>
    /// ハイスコア
    /// </summary>
    public int HighScore
    {
        get
        {
            return m_high_score;
        }
    }

    /// <summary>
    /// スコアの加算処理
    /// </summary>
    /// <param name="t_add_score">加算するスコア</param>
    public void AddScore(int t_add_score)
    {
        m_score += t_add_score;
        if (m_score > m_high_score)
        {
            m_high_score = m_score;
        }
    }

    /// <summary>
    /// リセットスコア
    /// </summary>
    public void Reset()
    {
        m_score = 0;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// スコアをアニメーションさせる
    /// </summary>
    /// <param name="startScore">始めのスコア</param>
    /// <param name="endScore">最終的なスコア</param>
    /// <param name="duration">最終的なスコアがでるまでの時間</param>
    /// <param name="scoreText">表示するテキスト</param>
    /// <returns></returns>
    public IEnumerator ScoreAnimation(float startScore, float endScore, float duration,Text scoreText)
    {
        // 開始時間
        float startTime = Time.time;

        // 終了時間
        float endTime = startTime + duration;

        while(Time.time < endTime)
        {
            // 現在の時間の割合
            float timeRate = (Time.time - startTime) / duration;

            // 数値を更新
            float updateValue = (float)((endScore - startScore) * timeRate + startScore);

            // テキストの更新
            scoreText.text = updateValue.ToString("f0"); // （"f0" の "0" は、小数点以下の桁数指定）

            // 1フレーム待つ
            yield return null;
        }

        // 最終的な着地のスコア
        scoreText.text = endScore.ToString();
    }
}
