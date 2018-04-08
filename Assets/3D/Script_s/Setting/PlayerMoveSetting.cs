using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoveSetting : SingletonMonoBehaviour<PlayerMoveSetting>
{
    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    //親オブジェクト取得用変数
    GameObject parentObj;

    //演出中か否か
    public bool MoveSettingFlg = true;

    //現在シーン取得用変数
    string nowScene;

    //エントリー時にPlayerオブジェクトを入れる変数
    GameObject entryPlayer;

    //メイン画面にキャラクターを生成　trueにすると生成できる
   bool createFilst = true;

    //プレイヤーPrefab
    public GameObject[] playerPre;

    //現在のプレイヤー人数
    public static int playerCount;
	
	void Update () {

        //プレイヤーの親オブジェクトを探す
        parentObj = GameObject.FindGameObjectWithTag("PlayerController");

        //現在シーンの名前を取得
        nowScene = SceneManager.GetActiveScene().name;

        //キャラクター選択画面だったら
        if (nowScene == "StageSelect")
        {
            //キャラクター生成モード
            createFilst = true;

            //Aまたはspaceが押された　かつ　現在の参加人数が最大人数を超えていない場合
            if (Input.GetButtonDown("JoyStick_Action1") && playerCount < playerPre.Length)
            {
                //playerPre配列に入っているオブジェクトを順番にentryPlayerに入れる
                entryPlayer = playerPre[playerCount];
                PlayerSetting playerSetting = entryPlayer.GetComponent<PlayerSetting>();

                //どのコントローラーでエントリーしたかを判別し、何番目のプレイヤーを操作するか設定
                playerSetting.playerNumber = JoyStickName();

                //すでにエントリーしている場合は、処理を中断
                foreach (Transform child in parentObj.transform)
                {
                    //子オブジェクトのplayerNumberを取得
                    PlayerSetting childSetting = child.GetComponent<PlayerSetting>();

                    //子オブジェクトと今エントリーしたオブジェクトのplayerNumber比べる
                    //一緒だった場合は、すでにエントリーされているため処理を中断する
                    if (childSetting.playerNumber == playerSetting.playerNumber) return;
                }

                //エントリー時の音を再生
                AudioManager.Instance.PlaySE("cursor");

                //エントリーしたプレイヤーオブジェクトを生成する
                Instantiate(entryPlayer, parentObj.transform);

                //プレイヤーNoを増やす
                playerCount++;
            }
        }
        //ゲームシーンだったら
        else if ((nowScene == "Main" || nowScene == "Main_Hard" || nowScene == "Main_Easy") && createFilst)
        {
            //キャラクター選択シーンで登録したオブジェクトを生成する
            for (int i = 0; i < playerCount; i++)
            {
                Instantiate(playerPre[i], parentObj.transform);
            }
            //生成は一度のみ
            createFilst = false;
        }
        //それ以外のシーンなら
        else
        {
            //プレイヤーを生成しない
            playerCount = 0;
        }
	}

    /// <summary>
    /// プレイヤーオブジェクトのplayerNumberを変更する
    /// </summary>
    /// <returns>登録するplayerNumber</returns>
    PlayerNumber JoyStickName()
    {
        if (Input.GetButtonDown("JoyStick_1_Action1")) return PlayerNumber.One;
        if (Input.GetButtonDown("JoyStick_2_Action1")) return PlayerNumber.Two;
        if (Input.GetButtonDown("JoyStick_3_Action1")) return PlayerNumber.Three;
        if (Input.GetButtonDown("JoyStick_4_Action1")) return PlayerNumber.Four;

        return PlayerNumber.KeyOne;
    }


}