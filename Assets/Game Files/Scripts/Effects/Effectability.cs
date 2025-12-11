using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;


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

        public void UseEffect()
        {
            ParticleSystem e = effect;
            float length = effectLength;

            ParticleSystem.MainModule main = e.main;
            main.loop = false;
            main.duration = effectLength;

            e.gameObject.SetActive(true);
            e.Play();
            if (effect.gameObject.Has(out AudioSource source)) source.Play();
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

