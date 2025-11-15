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

    private void Awake()
    {
        anims = this.Get<AnimationController>();
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
        this.Log("attack");
        anims.AnimateAttack();
    }
}
