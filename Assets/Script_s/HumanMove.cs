using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMove : MonoBehaviour {

    //逃げる速度
    public float speed = 0.05f;

    //humanの逃げる場所
    [Header("北、南、西、東")]
    public Transform[] createPos;

    Transform humanPos;

    DirectionType dirType;

    void Start () {

        dirType = transform.parent.GetComponent<DirectionType>();

	}

	void Update ()
    {
        switch(dirType.type)
        {
            case Direction.Noth:
                humanPos = createPos[0].transform;
                break;

            case Direction.South:
                humanPos = createPos[1].transform;
                break;

            case Direction.West:
                humanPos = createPos[2].transform;
                break;

            case Direction.East:
                humanPos = createPos[3].transform;
                break;
        }

        //逃げる方向にhumanを向かせる
        transform.rotation =
            Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(humanPos.position - transform.position), 0.3f);

        StartCoroutine(HumanWaitTime());
    }

    /// <summary>
    /// 人間が走ってい逃げていく処理
    /// </summary>
    /// <returns></returns>
    IEnumerator HumanWaitTime()
    {
        yield return new WaitForSeconds(1.0f);
        transform.localPosition += transform.forward * speed;
    }
}
