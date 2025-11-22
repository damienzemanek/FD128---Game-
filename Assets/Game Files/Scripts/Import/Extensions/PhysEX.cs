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
        public struct MoveSettings
        {
            public float origLienarSpeedMult;
            public float linearSpeedMult;
            public float decreaseMultOfSpeed;
            public float maxVel;
        }
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
            public bool addRbForward;
            [ShowIf("addRbForward")] public float fwdMult;
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
            if (jump.addRbForward)
                dir += rb.transform.forward * jump.fwdMult;

            dir += jump.direction.WithScale(jump.mult);

            rb.AddForce(dir, jump.forceMode);
        }

        public static void InputDirectionalMove(this Rigidbody rb, EntityControls controls, MoveSettings move)
        {
            Vector2 moveInput = (controls != null) ? controls.move.Invoke() : Vector2.zero;
            if (moveInput == Vector2.zero) return;

            float speedMult = move.linearSpeedMult;

            //if diagnally inputting
            if (Mathf.Abs(moveInput.x) > 0.5f && Mathf.Abs(moveInput.y) > 0.5f) 
                speedMult = speedMult * move.decreaseMultOfSpeed;


            if (moveInput.x != 0)
            {
                if (moveInput.x > 0.5)
                    rb.AddForce(controls.bodyDirection.transform.right * speedMult * 100, ForceMode.Force);
                if (moveInput.x < -0.5)
                    rb.AddForce(-controls.bodyDirection.transform.right * speedMult * 100, ForceMode.Force);
            }
            if (moveInput.y != 0)
            {
                if (moveInput.y > 0.5)
                    rb.AddForce(controls.bodyDirection.transform.forward * speedMult * 100, ForceMode.Force);
                if (moveInput.y < -0.5)
                    rb.AddForce(-controls.bodyDirection.transform.forward * speedMult * 100, ForceMode.Force);
            }
        }

    }
}
