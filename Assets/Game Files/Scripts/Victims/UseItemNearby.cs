using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEditor.Build;
using UnityEngine;
using static Effectability;
using static Extensions.AnimEX;

public class UseItemNearby : MonoBehaviour
{
    [SerializeField] Animatable eatAnims;
    [SerializeField] EffectUser loveEffect;

    [ShowInInspector] bool inCollision;
    [SerializeField, ReadOnly] Collider cached;
    [SerializeField] AgentAI agent;
    [SerializeField] MoveToObject move;
    [SerializeField] BunnyEat bunnyEat;
    [SerializeField] BunnyEat.EatZone zone = BunnyEat.EatZone.None;

    private void Start()
    {
        if (!agent.HasBelief(new IsObjectIWantNearby(), out BeliefAI _nearby)) return; IsObjectIWantNearby nearby = (IsObjectIWantNearby)_nearby;
        if (nearby.immediateAction is not MoveToObject _move) return;
        move = _move;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.Has(out UsableItem item)) return;

        inCollision = true;
        cached = other;
        if (item.Use()) loveEffect.UseEffect();
        this.Log("Staying");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.Has(out UsableItem item)) return;

        inCollision = false;
        cached = null;
        this.Log("Exiting");
    }

    private void FixedUpdate()
    {
        if(inCollision && cached != null)
        {
            if(!bunnyEat.eating) bunnyEat.StartEating(zone);
        }
        else
        {
            bunnyEat.StopEating(zone);
            cached = null;
            inCollision = false;
        }
    }

}
