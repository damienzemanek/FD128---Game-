using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using static CoroutineUtility;
using static Extensions.AudioEX;


public static class Effectability
{
    public static void UseEffectsAll(this EffectUser[] effects)
    {
        foreach (EffectUser effect in effects)
            effect.UseEffect();
    }

    public static void UseEffectsAllWithDelayInbetween(this EffectUser[] effects, MonoBehaviour host, float delay) => host.StartCoroutine(C_UseEffectsAllWithDelayInbetween(effects, delay));
    public static IEnumerator C_UseEffectsAllWithDelayInbetween(this EffectUser[] effects, float delay)
    {
        foreach (EffectUser effect in effects)
        {
            effect.UseEffect();
            yield return new WaitForSeconds(delay);
        }
    }


    [Serializable]
    public struct EffectUser
    {
        [SerializeField] ParticleSystem effect;
        [SerializeField] float effectLength;
        [SerializeField] float delay;
        [SerializeField] bool audio;
        [ShowIf("audio")][SerializeField] AudioSource source;
        [ShowIf("audio")][SerializeField] AudioClip clip;

        [Button]
        public void UseEffect()
        {
            if (effect == null) return;
            ParticleSystem e = effect;

            bool _audio = audio && (source != null) && (clip != null);
            AudioSource _source = source;
            AudioClip _clip = clip;


            ParticleSystem.MainModule main = e.main;
            main.loop = false;
            main.duration = effectLength;

            void EffectPlay()
            {
                if (_audio) _source.Play(_clip);
                e.gameObject.SetActive(true);
                e.Play();
            }

            if (delay <= 0f)    EffectPlay();
            else                DelayUtility.Delay(EffectPlay, delay);


        }
    }

    [Serializable]
    public struct EffectObjectRagdoll
    {
        [SerializeField] GameObject[] prePlacedObjects;
        [SerializeField] float lifetime;

        public void UseEffect()
        {
            prePlacedObjects.SetAllActive(true);
            foreach (GameObject obj in prePlacedObjects)
                GameObject.Destroy(obj, lifetime);
        }
    }

}

