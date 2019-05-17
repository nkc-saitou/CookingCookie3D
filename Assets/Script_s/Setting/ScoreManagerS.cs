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
    /// ハイスコアを更新する
    /// </summary>
    public void ScoreRenew()
    {
        if(m_score > m_high_score)
        {
            m_high_score = m_score;
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
}
