using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using Sirenix.OdinInspector;

public class Bunnyhop : MonoBehaviour
{
    PlayerDataHolder player;
    Rigidbody rb;
    [ShowInInspector, ReadOnly] bool isGrounded;

    public PhysEX.GroundedSettings ground;
    public PhysEX.FallSettings fall;
    public PhysEX.JumpSettings jump;

    private void Awake()
    {
        player = PlayerDataHolder.Instance;
        rb = this.Get<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) rb.Jump(jump);
    }

    private void FixedUpdate()
    {
        isGrounded = transform.IsGrounded(ground);

        if (!isGrounded)
        {
            rb.drag = player.data.fallDrag;
            rb.FallFaster(fall);
        }
        else
            rb.drag = player.data.groundedDrag;

    }
}
