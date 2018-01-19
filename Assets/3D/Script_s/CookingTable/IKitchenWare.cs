using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 調理器具限定
/// </summary>
public interface IKitchenWare : IExecutable {

    List<CookingMaterial> elemLis
    {
        get;
    }

    float CheckProgress();
}