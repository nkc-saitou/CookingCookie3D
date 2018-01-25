using System.Collections;
using System;
using UnityEngine;

/// <summary>
/// 待機時間を生成する機能
/// </summary>
public class MonoBehaviourExtension : MonoBehaviour
{
    protected void WaitAfter(float _wait, Action _act)
    {
        if (_act != null)
        {
            StartCoroutine(_WaitAfter(_wait, _act));
        }
    }

    IEnumerator _WaitAfter(float _wait, Action _act)
    {
        yield return new WaitForSeconds(_wait);

        _act();
    }
}