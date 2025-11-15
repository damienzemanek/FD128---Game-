using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void Animate(string name)
    {
        animator.Play(name);
    }

    public void AnimateThen(string name, Action postHook) => animator.PlayWithHook(name, this, postHook);

}
