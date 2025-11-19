using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class RigidbodyGrabber : MonoBehaviour
{
    Rigidbody connectedRb;
    ConfigurableJoint joint;

    private void Awake()
    {
        joint = this.Get<ConfigurableJoint>();
        connectedRb = RigidbodyToBeGrabbed.Instance.Grab(joint);
    }

    private void OnEnable()
    {
        joint.connectedBody = connectedRb;
    }
}
