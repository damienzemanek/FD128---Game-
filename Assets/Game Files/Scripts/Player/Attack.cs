using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using Extensions;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class Attack : MonoBehaviour
{
    [Inject] EntityControls controls;

    [TitleGroup("Parameters")] [SerializeField] bool bloodiedLeftHand;
    [TitleGroup("Parameters")][SerializeField] bool bloodiedRightHand;

    [TitleGroup("Parameters")][SerializeField] bool onRightHand;


    [TitleGroup("Refs")] [SerializeField, ReadOnly] AnimationController anims;
    [TitleGroup("Refs")] [SerializeField] AttackTrigger attackTrigger;
    [TitleGroup("Refs")] [SerializeField] GameObject bloodyHandLeft;
    [TitleGroup("Refs")][SerializeField] GameObject bloodyHandRight;


    [TitleGroup("Anims")] [SerializeField] string leftAttackName = "attackLeft";
    [TitleGroup("Anims")] [SerializeField] string rightAttackName = "attackRight";


    private void Awake()
    {
        anims = this.Get<AnimationController>();
        attackTrigger.Ensure(this);
        attackTrigger.attack = this;
    }


    private void OnEnable()
    {
        DisableBloodyHands();
        onRightHand = true;
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

        if(onRightHand) anims.AnimateThen(rightAttackName, DisableAttacking);
        else            anims.AnimateThen(leftAttackName, DisableAttacking);

        onRightHand = !onRightHand;
    }

    public void EnableAttacking() => attackTrigger.attacking = true;

    public void DisableAttacking() => attackTrigger.attacking = false;

    public void EnableBloodyHands()
    {
        if (!onRightHand)
        {
            if (bloodiedRightHand) return;
            bloodiedRightHand = true;
            bloodyHandRight.SetActive(true);
        }
        else
        {
            if (bloodiedLeftHand) return;
            bloodiedLeftHand = true;
            bloodyHandLeft.SetActive(true);
        }
    }
    public void DisableBloodyHands()
    {
        bloodiedLeftHand = false;
        bloodiedRightHand = false;
        bloodyHandRight.SetActive(false);
        bloodyHandLeft.SetActive(false);
    }

}
