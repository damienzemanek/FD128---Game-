using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

public class ConstantLookAt : MonoBehaviour
{
    [SerializeField] public bool looking;
    [SerializeField] bool lookAtFunc;
    [SerializeField] bool explicitVectors;
    [SerializeField, ShowIf("explicitVectors")] bool useX;
    [SerializeField, ShowIf("explicitVectors")] bool useY;
    [SerializeField, ShowIf("explicitVectors")] bool useZ;

    [SerializeField] Transform lookAtTransform;


    private void Awake()
    {
        looking = true;
        lookAtTransform.Ensure(this);
    }
    private void Update()
    {
        if (!looking) return;

        if(lookAtFunc)
            transform.LookAt(lookAtTransform.position);
        else
        {
            Vector3 dirToLookAtObj = lookAtTransform.position - transform.position;

            Quaternion lookAtRot = Quaternion.LookRotation(dirToLookAtObj);
            Vector3 lookAtRotVector3 = lookAtRot.eulerAngles;

            float x = useX ? lookAtRotVector3.x : transform.eulerAngles.x;
            float y = useY ? lookAtRotVector3.y : transform.eulerAngles.y;
            float z = useZ ? lookAtRotVector3.z : transform.eulerAngles.z;

            transform.rotation = Quaternion.Euler(x, y, z);
        }
    }
}
