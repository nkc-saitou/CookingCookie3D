using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    //---------------------------------------------------------
    // private
    //---------------------------------------------------------
    private const string BGM_VOLUME_KEY = "BGM_VOLUME_KEY";
    private const float BGM_VOLUME_DEFULT = 1.0f;

    private float _bgmFadeSpeedRate = FADE_SPEED_RATE_HIGH;

    string nextName_BGM;
    string nextName_SE;

    bool isFadeOut = false;

    Dictionary<string, AudioClip> _seDic,_bgmDic;

    //--------------------------------------------------------
    // public 
    //--------------------------------------------------------

    //フェード時間
    public const float FADE_SPEED_RATE_HIGH = 0.7f;
    public const float FADE_SPEED_RATE_LOW = 0.15f;

    public AudioSource attachSESource,attachBGMSource;


    void Awake()
    {
        //インスタンスがない場合
        if(this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        //SEのファイルを読み込んでセットする
        _seDic = new Dictionary<string, AudioClip>();
        _bgmDic = new Dictionary<string, AudioClip>();

        //Resourcesの中にあるSE,BGMフォルダの中身を全部読み込む
        object[] seList = Resources.LoadAll("SE");
        object[] bgmList = Resources.LoadAll("BGM");

        foreach(AudioClip se in seList)
        {
            _seDic[se.name] = se;
        }

        foreach(AudioClip bgm in bgmList)
        {
            _bgmDic[bgm.name] = bgm;
        }
    }

    //--------------------------------------------------------
    // SE
    //--------------------------------------------------------

    /// <summary>
    /// ＳＥを再生
    /// </summary>
    /// <param name="seName">ＳＥの名前</param>
    /// <param name="delay">遅延させる時間。ない場合は設定しなくてよい</param>
    public void PlaySE(string seName,float delay = 0.0f)
    {
        //SEがなかったら終わり
        if(!_seDic.ContainsKey(seName))
        {
            return;
        }

        //流すＳＥを設定する
        nextName_SE = seName;
        Invoke("DelayPlaySE", delay);
    }

    void DelayPlaySE()
    {
        //ＳＥを流す
        attachSESource.PlayOneShot(_seDic[nextName_SE] as AudioClip);
    }

    //--------------------------------------------------------
    // BGM
    //--------------------------------------------------------

    /// <summary>
    /// ＢＧＭを流す
    /// </summary>
    /// <param name="bgmName">ＢＧＭの名前</param>
    /// <param name="fadeSpeedRate">ＢＧＭのフェードスピード</param>
    public void PlayBGM(string bgmName,float fadeSpeedRate = FADE_SPEED_RATE_HIGH)
    {
        //ＢＧＭがない場合は終了
        if(!_bgmDic.ContainsKey(bgmName))
        {
            return;
        }

        //ＢＧＭが流れていない場合
        if(!attachBGMSource.isPlaying)
        {
            //流す
            nextName_BGM = "";
            attachBGMSource.clip = _bgmDic[bgmName] as AudioClip;
            attachBGMSource.Play();
        }
        //別のＢＧＭが流れている場合
        else if(attachBGMSource.clip.name != bgmName)
        {
            //前のＢＧＭをフェードアウトさせる
            nextName_BGM = bgmName;
            FadeOutBGM(fadeSpeedRate);
        }
    }

    public void FadeOutBGM(float fadespeedRate = FADE_SPEED_RATE_LOW)
    {
        _bgmFadeSpeedRate = fadespeedRate;
        isFadeOut = true;
    }

    void Update()
    {
        if(!isFadeOut)
        {
            attachBGMSource.volume = 0.2f;
            return;
        }

        attachBGMSource.volume -= Time.deltaTime * _bgmFadeSpeedRate;

        if(attachBGMSource.volume <= 0)
        {
            attachBGMSource.Stop();
            attachBGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
            isFadeOut = false;

            if(!string.IsNullOrEmpty(nextName_BGM))
            {
                PlayBGM(nextName_BGM);
            }
        }
    }
}
