using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using Sirenix.OdinInspector;
using Extensions;
using static Extensions.AudioEX;

public class Bunnyhop : MonoBehaviour
{
    PlayerDataHolder player;
    Rigidbody rb;
    [ShowInInspector, ReadOnly] bool isGrounded;

    [SerializeField] PhysEX.GroundedSettings ground;
    [SerializeField] PhysEX.FallSettings fall;
    [ShowInInspector, ReadOnly] PhysEX.JumpSettings jump { get => player == null ? (default) : player.data.jump; }

    public AudioSource source;
    public AudioClip[] sfx_jump;
    public AudioClip[] sfx_fall;
    public AudioStepper audStepper;
    public bool landed = true;

    private void Awake()
    {
        player = PlayerDataHolder.Instance;
        rb = this.Get<Rigidbody>();
        landed = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.Jump(jump);
            source.Play(sfx_jump.Rand());
        }
    }

    private void FixedUpdate()
    {
        isGrounded = transform.IsGrounded(ground);

        if (!isGrounded)
        {
            landed = false;
            rb.drag = player.data.fallDrag;
            rb.FallFaster(fall);
            audStepper.blocked = true;
        }
        else
        {
            Land();
            audStepper.blocked = false;
            rb.drag = player.data.groundedDrag;
        }

    }

    void Land()
    {
        if (landed) return;
        landed = true;
        source.Play(sfx_fall.Rand());
    }
}
