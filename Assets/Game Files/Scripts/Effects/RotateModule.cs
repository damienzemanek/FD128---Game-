using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RotateExtension;
using Random = UnityEngine.Random;

public class RotateModule : MonoBehaviour
{
    public ConstantRotate ConstantRotateModule;
    public RandomRotation RandomRotationModule;

    private void Start()
    {
        if (RandomRotationModule.active && RandomRotationModule.rotateOnSpawn) RandomRotationModule.Rotate();
    }

    private void FixedUpdate()
    {
        if (ConstantRotateModule.active) ConstantRotateModule.Rotate();
    }
}

public static class RotateExtension
{

    [Serializable]
    public struct ConstantRotate
    {
        [SerializeField] public bool active;
        [SerializeField] Vector3 rotation;
        [SerializeField] Transform transform;

        public void Rotate() => transform.Rotate(rotation); 
    }

    [Serializable]
    public struct RandomRotation
    {
        [SerializeField] public bool active;
        public bool x, y, z;
        [SerializeField] Transform transform;

        public bool rotateOnSpawn;

        public void Rotate()
        {
            Vector3 r = transform.eulerAngles;

            if(x) r.x = Random.Range(0, 360);
            if(y) r.y = Random.Range(0, 360);
            if(z) r.z = Random.Range(0, 360);

            transform.rotation = Quaternion.Euler(r);
        }

    }

}
