using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using static Extensions.AnimEX;

[DefaultExecutionOrder(1)]
public class Attack : MonoBehaviour
{
    [Inject] EntityControls controls;

    [TitleGroup("Parameters")] 
    [SerializeField] bool bloodiedLeftHand;
    [SerializeField] bool bloodiedRightHand;
    [SerializeField] bool onRightHand;


    [TitleGroup("Refs")] 
    [SerializeField] AttackTrigger attackTrigger;
    [SerializeField] MaterialSetter leftHandMats;
    [SerializeField] MaterialSetter rightHandMats;



    [TitleGroup("Anims")]
    [SerializeField] Animatable anims;
    [SerializeField] string leftAttackName = "attackLeft";
    [SerializeField] string rightAttackName = "attackRight";



    private void Awake()
    {
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

        if(onRightHand) anims.Animate(rightAttackName, this, DisableAtacking);
        else            anims.Animate(leftAttackName, this, DisableAtacking);

        onRightHand = !onRightHand;
    }

    public void EnableAttacking() => attackTrigger.attacking = true;
    public void DisableAtacking() => attackTrigger.attacking = false;


    public void EnableBloodyHands()
    {
        if (!onRightHand)
        {
            if (bloodiedRightHand) return;
            bloodiedRightHand = true;
            rightHandMats.SetMatToIndex(1);
        }
        else
        {
            if (bloodiedLeftHand) return;
            bloodiedLeftHand = true;
            leftHandMats.SetMatToIndex(1);
        }
    }
    public void DisableBloodyHands()
    {
        bloodiedLeftHand = false;
        bloodiedRightHand = false;
        rightHandMats.SetMatToIndex(0);
        leftHandMats.SetMatToIndex(0);
    }

}
