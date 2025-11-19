using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotate : MonoBehaviour
{
    [SerializeField] Vector3 rotation;

    private void FixedUpdate()
    {
        transform.Rotate(rotation);
    }
}


