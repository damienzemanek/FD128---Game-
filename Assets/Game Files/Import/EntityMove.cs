using Sirenix.OdinInspector;
using UnityEngine;
using DependencyInjection;
using Extensions;

[DefaultExecutionOrder(1)]
[RequireComponent(typeof(EntityControls))]
public class EntityMove : MonoBehaviour
{
    [Inject] EntityControls Controls;
    PlayerDataHolder player;

    Rigidbody rb;
    [SerializeField] float speedMultiplier; float origSpeed;
    [ShowInInspector, ReadOnly] float maxVel { get => player == null ? 0 : player.data.maxVel; }

    [Button]
    void UpdateOrigSpeed()
    {
        origSpeed = speedMultiplier;
    }

    private void Awake()
    {
        player = PlayerDataHolder.Instance;

        if(rb == null) rb = GetComponent<Rigidbody>();
        if (Controls == null) Debug.LogError("No Controls found");
        UpdateOrigSpeed();
    }





    private void FixedUpdate()
    {
        UpdateSpeed();
        MoveEntity();
    }

    void UpdateSpeed()
    {
        rb.maxLinearVelocity = maxVel;
        rb.maxAngularVelocity = maxVel;
    }

    void MoveEntity()
    {

        Vector2 moveInput = (Controls != null) ? Controls.move.Invoke() : Vector2.zero;
        if (moveInput == Vector2.zero) return;

        if (Mathf.Abs(moveInput.x) > 0.5f && Mathf.Abs(moveInput.y) > 0.5f)
            speedMultiplier = origSpeed * 0.7f;
        else
            speedMultiplier = origSpeed;


        if (moveInput.x != 0)
        {
            if (moveInput.x > 0.5)
                rb.AddForce(Controls.bodyDirection.transform.right * speedMultiplier * 100, ForceMode.Force);
            if (moveInput.x < 0.5)
                rb.AddForce(-Controls.bodyDirection.transform.right * speedMultiplier * 100, ForceMode.Force);
        }
        if (moveInput.y != 0)
        {
            if (moveInput.y > 0.5)
                rb.AddForce(Controls.bodyDirection.transform.forward * speedMultiplier * 100, ForceMode.Force);
            if (moveInput.y < 0.5)
                rb.AddForce(-Controls.bodyDirection.transform.forward * speedMultiplier * 100, ForceMode.Force);
        }
    }




}
