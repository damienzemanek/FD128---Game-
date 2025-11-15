using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    PlayerDataHolder player;
    [SerializeField] bool _attacking;
    [SerializeField] bool onHitCooldown;
    [SerializeField] float hitCooldown = 1f;

    public bool attacking { get => _attacking; set => _attacking = value; }

    private void Awake()
    {
        player = PlayerDataHolder.Instance;
        attacking = false;
        onHitCooldown = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag != "Person") return;
        print("a");
        if (!other.Has(out Health hp)) return;
        print("b");
        if (!attacking) return;
        print("c");
        if (onHitCooldown) return;
        print("d");
        print(hp);
        print(player);
        print(player.data);

        hp.TakeDmg(player.data.dmg);
        onHitCooldown = true;

        this.StopAllCoroutines();
        this.DelayedCall(() => onHitCooldown = false, hitCooldown);
    }

}
