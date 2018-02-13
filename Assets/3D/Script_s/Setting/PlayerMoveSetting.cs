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

    GameObject parentObj;

    //演出中か否か
    public bool MoveSettingFlg = true;

    string nowScene;

    //bool filst = true;
    GameObject player;

    /// <summary>
    /// メイン画面にキャラクターを生成　trueにすると生成できる
    /// </summary>
    [System.NonSerialized]
    public bool filst = true;

    bool playerCreateFilst = true;
    //public GameObject playerSetting;

    public GameObject[] playerPre;

    [System.NonSerialized]
    public int playerCount;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        parentObj = GameObject.FindGameObjectWithTag("PlayerController");
        nowScene = SceneManager.GetActiveScene().name;

        //Debug.Log(filst);

        if (nowScene == "StageSelect")
        {
            filst = true;
            playerCreateFilst = false;
            if (Input.GetButtonDown("JoyStick_Action1") && playerCount < playerPre.Length)
            {
                player = playerPre[playerCount];
                PlayerSetting playerSetting = player.GetComponent<PlayerSetting>();
                playerSetting.playerNumber = JoyStickName();

                foreach (Transform child in parentObj.transform)
                {
                    PlayerSetting childSetting = child.GetComponent<PlayerSetting>();
                    if (childSetting.playerNumber == playerSetting.playerNumber) return;
                }

                if (MoveSettingFlg == false) MoveSettingFlg = true;

                AudioManager.Instance.PlaySE("cursor");
                Instantiate(player, parentObj.transform);
                playerCount++;
            }
        }
        else if ((nowScene == "Main" || nowScene == "Main_hard") && filst)
        {
            for (int i = 0; i < playerCount; i++)
            {
                Debug.Log(playerPre[i]);
                Instantiate(playerPre[i], parentObj.transform);
            }
            filst = false;
        }
        else
        {
            playerCount = 0;
        }
	}


    PlayerNumber JoyStickName()
    {
        if (Input.GetButtonDown("JoyStick_1_Action1")) return PlayerNumber.One;
        if (Input.GetButtonDown("JoyStick_2_Action1")) return PlayerNumber.Two;
        if (Input.GetButtonDown("JoyStick_3_Action1")) return PlayerNumber.Three;
        if (Input.GetButtonDown("JoyStick_4_Action1")) return PlayerNumber.Four;

        return PlayerNumber.KeyOne;
    }


}