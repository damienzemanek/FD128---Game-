using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModule : MonoBehaviour
{
    public ConstantRotate ConstantRotateModule;

    private void FixedUpdate()
    {
        if(ConstantRotateModule.active)
    }
}

public static class RotateExtension
{

    public struct ConstantRotate
    {
        [SerializeField] public bool active;
        [SerializeField] Vector3 rotation;
    }

}
