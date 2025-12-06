using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using Extensions;
using static Entity;

public class AttackTrigger : MonoBehaviour
{
    PlayerDataHolder player;
    [SerializeField] public Attack attack;
    [SerializeField] bool _attacking;
    [SerializeField] bool _onHitCooldown;
    [SerializeField] float hitCooldown = 1f;

    public bool attacking { get => _attacking; set => _attacking = value; }
    public bool onHitCooldown { get => _onHitCooldown; set => _onHitCooldown = value; }

    private void Awake()
    {
        player = PlayerDataHolder.Instance;
        attacking = false;
        onHitCooldown = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out IHittable h)) return;
        if (!IsAttacking()) return;

        h.Hit(player.data.dmg);
        if(h.hittable.isFleshy) attack.EnableBloodyHands();
        onHitCooldown = true;

        this.StopAllCoroutines();
        this.DelayedCall(() => onHitCooldown = false, hitCooldown);
    }

    public bool IsAttacking() => (attacking) && (!onHitCooldown);

}
