using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class PhysEX
    {
        [Serializable]
        public struct GroundedSettings
        {
            public Transform feetPoint;
            public float checkDist;
            public LayerMask mask;
        }

        [Serializable]
        public struct FallSettings
        {
            public ForceMode forceMode;
            public float mult;
            public Vector3 dir;
        }

        [Serializable]
        public struct JumpSettings
        {
            public bool addBodyDirection;
            [ShowIf("addBodyDirection")] public float bodyDirMult;
            [ShowIf("addBodyDirection")] public Transform body;
            public Vector3 direction;
            public Vector3 mult;
            public ForceMode forceMode;
        }




        public static bool IsGrounded(this Transform transform, GroundedSettings ground)
        {
            bool isGrounded = Physics.Raycast(ground.feetPoint.position,
                                        -transform.up,
                                        out RaycastHit hit,
                                        ground.checkDist,
                                        ground.mask);

            return isGrounded;
        }

        public static void FallFaster(this Rigidbody rb, FallSettings fall)
        {
            rb.AddForce(fall.dir * fall.mult, fall.forceMode);
        }

        public static void Jump(this Rigidbody rb, JumpSettings jump)
        {
            Vector3 dir = Vector3.zero;
            if (jump.addBodyDirection)
                dir += jump.body.forward * jump.bodyDirMult;

            dir += jump.direction.WithScale(jump.mult);

            rb.AddForce(dir, jump.forceMode);
        }

    }
}
