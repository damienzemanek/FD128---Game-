using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace Extensions
{
    public static class AnimEX
    {
        [Serializable]
        public struct Animatable
        {
            [SerializeField] public Animator animator;
            public Deviatable animSpeed;

            public void Animate(string _animName, MonoBehaviour host = null, Action postHook = null, int layer = 0)
            {
                if (animator == null) { Debug.LogWarning("Early Return: NO Animator found"); return; }

                if (postHook != null)
                {
                    if (host == null) this.Error("Need a Monobehaviour host parameter to use PostHook");
                    animator.PlayWithHook(_animName, host, postHook, layer);
                    this.Log($"Playing anim : {_animName} ");
                }
                else
                {
                    this.Log($"Playing anim : {_animName} ");
                    animator.Play(_animName, layer);
                }

                if (animSpeed.deviate)
                    animator.speed = animSpeed.value;

            }

            public void CrossFade(string _animName, MonoBehaviour host = null, Action postHook = null, float fade = 0.25f, int layer = 0)
            {
                if (animator == null) { Debug.LogWarning("Early Return: NO Animator found"); return; }

                if (postHook != null)
                    animator.CrossFadeWithHook(_animName, host, postHook, fade, layer);
                else
                    animator.CrossFade(_animName, fade, layer);

                if (animSpeed.deviate)
                    animator.speed = animSpeed.value;

            }
        }

        public static void PlayWithHook(this Animator animator, string statename, MonoBehaviour mono, Action postHook, int layer = 0)
            => mono.StartCoroutine(C_PlayWithHook(animator, statename, mono, postHook, layer));

        public static IEnumerator C_PlayWithHook(this Animator animator, string statename, MonoBehaviour mono, Action postHook, int layer = 0)
        {
            animator.Play(statename, layer);

            yield return null;

            AnimatorStateInfo animInfo = animator.GetCurrentAnimatorStateInfo(0);
            float duration = animInfo.length / animator.speed;

            yield return new WaitForSeconds(duration);

            postHook?.Invoke();
        }

        public static void CrossFadeWithHook(this Animator animator, string statename, MonoBehaviour mono, Action postHook, float fade = 0.25f, int layer = 0)
    => mono.StartCoroutine(C_CrossFadeWithHook(animator, statename, mono, postHook, fade, layer));

        public static IEnumerator C_CrossFadeWithHook(this Animator animator, string statename, MonoBehaviour mono, Action postHook, float fade = 0.25f, int layer = 0)
        {
            animator.CrossFade(statename, fade, layer);

            yield return null;

            AnimatorStateInfo animInfo = animator.GetCurrentAnimatorStateInfo(0);
            float duration = animInfo.length / animator.speed;

            yield return new WaitForSeconds(duration);

            postHook?.Invoke();
        }
    }
}
