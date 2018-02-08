using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMove : MonoBehaviour {

    public float speed = 0.05f;
    [Header("北、南、西、東")]
    public Transform[] createPos; //humanの逃げる場所

    Transform humanPos;
    private Vector3 vec;

    DirectionType dirType;

    // Use this for initialization
    void Start () {

        dirType = transform.parent.GetComponent<DirectionType>();

        Debug.Log(dirType.type);

	}
	
	// Update is called once per frame
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

        transform.rotation =
            Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(humanPos.position - transform.position), 0.3f);

        StartCoroutine(waitTime());
    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(1.0f);
        transform.localPosition += transform.forward * speed;
    }
}
