using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public struct EffectUser
{
    [SerializeField] ParticleSystem effect;
    [SerializeField] float effectLength;

    [Button]
    public void UseEffect(MonoBehaviour host)
    {
        ParticleSystem e = effect;
        float length = effectLength;

        effect.gameObject.SetActive(true);
        effect.Play();
        if (effect.gameObject.Has(out AudioSource source)) source.Play();
        host.DelayedCall(() => e.Stop(), length);
    }
}
