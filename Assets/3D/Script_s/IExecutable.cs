using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーがアクションできるすべての行動
/// </summary>
public interface IExecutable {

    void SetElement(CookingMaterial mat); 
    CookingMaterial GetElement();

}