using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class Attack : MonoBehaviour
{
    [Inject] EntityControls controls;
    [SerializeField, ReadOnly] AnimationController anims;
    [SerializeField] AttackTrigger attackTrigger;

    [TitleGroup("Anims")]
    [SerializeField] string attackAnimName = "attack";

    private void Awake()
    {
        anims = this.Get<AnimationController>();
        attackTrigger.Ensure(this);
    }


    private void OnEnable()
    {
        controls.mouse1 += AttackDo;
    }

    private void OnDisable()
    {
        controls.mouse1 -= AttackDo;
    }


    public void AttackDo()
    {
        if (attackTrigger.attacking) return;

        this.Log("attack");
        EnableAttacking();
        anims.AnimateThen(attackAnimName, DisableAttacking);
    }

    public void EnableAttacking() => attackTrigger.attacking = true;

    public void DisableAttacking() => attackTrigger.attacking = false;

}
