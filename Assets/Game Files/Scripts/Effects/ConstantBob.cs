using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantBob : MonoBehaviour
{
    [SerializeField] float upAmount;
    [SerializeField] float moveTime = 0.5f;

    Vector3 origPos;
    Vector3 newPos;

    private void Awake()
    {
        origPos = transform.position;
        newPos = new Vector3(origPos.x, origPos.y + upAmount, origPos.z);
    }

    private void Start()
    {
        Up();
    }

    void Up()
    {
        StopAllCoroutines();
        transform.Lerp(newPos, moveTime, this, Down);
    }

    void Down()
    {
        StopAllCoroutines();
        transform.Lerp(origPos, moveTime, this, Up);
    }
}
