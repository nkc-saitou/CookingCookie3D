using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 調理器具限定
/// </summary>
public interface IKitchenWare : IExecutable {

    //置いた素材を格納
    List<CookingMaterial> elemLis
    {
        get;
    }

    //料理進行度をチェックする
    float CheckProgress();
}