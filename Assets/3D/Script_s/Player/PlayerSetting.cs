using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMove))]
public class PlayerSetting : MonoBehaviour {

    //----------------------------------------------------
    // public
    //----------------------------------------------------

    [SerializeField, Header("移動速度"), Range(0, 10)]
    public float speed = 3.0f;

    [SerializeField, Header("ゲームパッドでプレイするか")]
    public bool playIsGamePad = false;

    [System.NonSerialized]
    public int playerNum;

    [System.NonSerialized]
    public string keyMove_x; //x軸の仮想入力名を格納する変数

    [System.NonSerialized]
    public string keyMove_y; //ｙ軸の仮想入力名を格納する変数

    [System.NonSerialized]
    public string keyAction;

    [System.NonSerialized]
    public string keyAction_2;

    //----------------------------------------------------
    // 列挙型
    //----------------------------------------------------

    public enum PlayerNumber
    {
        One = 0,
        Two
    }

    public PlayerNumber playerNumber;

    void Start ()
    {
        GamePlayers();
        KeySetting();
    }

    //-----------------------------------
    // 対応した仮想入力キーの取得
    //-----------------------------------
    void KeySetting()
    {
        //ゲームパッドだった場合
        if (playIsGamePad)
        {
            keyMove_x = "L_JoyStick" + playerNum + "_XAxis";
            keyMove_y = "L_JoyStick" + playerNum + "_YAxis";
            keyAction = "JoyStick_" + playerNum + "_Action1";
            keyAction_2 = "JoyStick_" + playerNum + "_Action2";
        }
        else
        {
            keyMove_x = "Horizontal_" + playerNum;
            keyMove_y = "Vertical_" + playerNum;
            keyAction = "Fire" + playerNum;
            keyAction_2 = "Put_" + playerNum;
        }
    }

    //-----------------------------------
    // プレイ人数の取得
    //-----------------------------------
    void GamePlayers()
    {
        switch (playerNumber)
        {
            case PlayerNumber.One:
                playerNum = 1;
                break;

            case PlayerNumber.Two:
                playerNum = 2;
                break;
        }
    }
}
