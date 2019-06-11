using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーがどの机に対してでもできる行動
/// </summary>
public interface IExecutable {

    //素材を置く
    void SetElement(CookingMaterial mat); 

    //素材を取る
    CookingMaterial GetElement();

}