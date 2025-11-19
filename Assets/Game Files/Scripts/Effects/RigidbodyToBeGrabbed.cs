using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns.CreationalPatterns;
using Extensions;

public class RigidbodyToBeGrabbed : Singleton<RigidbodyToBeGrabbed>
{
    [SerializeField] CarrotFeeder feeder;
    public Rigidbody Grab(ConfigurableJoint joint)
    {
        feeder.GiveJoint(joint);
        return this.Get<Rigidbody>();
    }
}
