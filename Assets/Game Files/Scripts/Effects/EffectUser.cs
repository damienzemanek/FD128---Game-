using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

public class EffectUser : MonoBehaviour
{
    [SerializeField] ParticleSystem effect;
    [SerializeField] float effectLength = 4f;

    private void Awake()
    {
        effect.gameObject.SetActive(false);
    }

    [Button]
    public void UseEffect()
    {
        effect.gameObject.SetActive(true);
        effect.Play();
        if (effect.gameObject.Has(out AudioSource source)) source.Play();
        this.DelayedCall(() => effect.Stop(), effectLength);
    }
}
