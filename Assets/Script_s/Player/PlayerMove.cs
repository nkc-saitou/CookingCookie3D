using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMove : MonoBehaviour {

    public bool animWalkFlg { get; set; }

    //----------------------------------------------
    // private
    //----------------------------------------------
    Rigidbody rg;

    PlayerSetting playerSetting;

    float move_x = 0, move_z = 0; //ｘ、ｙの移動用変数
    float rotateSpeed = 5.0f; //回転スピード
    
    void Start ()
    {
        playerSetting = GetComponent<PlayerSetting>();
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(PlayerMoveSetting.Instance.MoveSettingFlg) Move();
    }

    //----------------------------------------------
    // 移動処理メソッド
    //----------------------------------------------
    void Move()
    {
        //x,yの移動係数を求める
        move_x = Input.GetAxis(playerSetting.keyMove_x) * playerSetting.speed;
        move_z = Input.GetAxis(playerSetting.keyMove_y) * playerSetting.speed;

        Vector3 dir = new Vector3(move_x, 0, move_z);

        //方向ベクトルがある一定以上の大きさだったら、進行方向にキャラの向きを変える
        if (dir.magnitude > 0.01f)
        {
            float step = rotateSpeed * Time.deltaTime;
            Quaternion playerRotate = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, playerRotate, step);

            //アニメーションの処理↓
            animWalkFlg = true;

        }
        else
        {
            animWalkFlg = false;
        }
        //アニメーションの処理↑

        rg.velocity = new Vector3(move_x, 0, move_z);
    }
}