// HOTRELOAD_IGNORE

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public struct Animatable
{
    [SerializeField] public Animator animator;

    public void Animate(string _animName, MonoBehaviour host = null, Action postHook = null)
    {
        if (animator == null) { Debug.LogWarning("Early Return: NO Animator found"); return; }

        if (postHook != null)
            animator.PlayWithHook(_animName, host, postHook);
        else
            animator.Play(_animName);
    }
}
