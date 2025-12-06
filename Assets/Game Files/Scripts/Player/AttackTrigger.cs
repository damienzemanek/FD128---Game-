using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using Extensions;
using static Entity;
using Sirenix.OdinInspector;

public class AttackTrigger : MonoBehaviour
{
    PlayerDataHolder player;
    [SerializeField, ReadOnly] public Attack attack;
    [SerializeField] bool _attacking;
    [SerializeField] bool _onHitCooldown;

    public bool attacking { get => _attacking; set => _attacking = value; }
    public bool onHitCooldown { get => _onHitCooldown; set => _onHitCooldown = value; }

    private void Awake()
    {
        player = PlayerDataHolder.Instance;
    }

    private void Start()
    {
        StopAttacking();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out IHittable h)) return;
        if (!IsAttacking()) return;

        StartAttacking(h);
        this.DelayedCall(StopAttacking, player.data.hitCooldown, true);
    }

    public void StartAttacking(IHittable h)
    {
        h.Hit(player.data.dmg);
        if (h.hittable.isFleshy) attack.EnableBloodyHands();
        onHitCooldown = true;
    }

    public void StopAttacking()
    {
        onHitCooldown = false;
        attacking = false;
    }
    public bool IsAttacking() => (attacking) && (!onHitCooldown);

}
