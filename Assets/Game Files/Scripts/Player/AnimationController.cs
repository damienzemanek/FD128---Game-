using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] public string attackAnimName;

    public void Animate(string name)
    {
        animator.Play(name);
    }

    public void AnimateAttack() => Animate(attackAnimName);

}
