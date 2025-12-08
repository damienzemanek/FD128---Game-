using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using static Effectability;

public class UseItemNearby : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string boolName;
    [SerializeField] EffectUser loveEffect;

    bool inCollision;
    [SerializeField, ReadOnly] Collider cached;

    private void OnTriggerStay(Collider other)
    {
        if (!other.Has(out UsableItem item)) return;

        inCollision = true;
        cached = other;
        animator.SetBool(boolName, true);
        if (item.Use()) loveEffect.UseEffect();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.Has(out UsableItem item)) return;

        inCollision = false;
        cached = null;
        animator.SetBool(boolName, false);
    }

    private void FixedUpdate()
    {
        if(inCollision && cached == null)
        {
            inCollision = false;
            cached = null;
            animator.SetBool(boolName, false);
        }

    }

}
