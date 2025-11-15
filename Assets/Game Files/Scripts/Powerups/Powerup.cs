using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.Utilities;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    EffectManager effects;
    [SerializeField] public float duration;
    [SerializeReference] Effect[] effect;

    private void Awake()
    {
        effects = EffectManager.Instance;
        effects.Ensure(this);
    }

    public void ObtainPowerup()
    {
        effect.ForEach(action: effect => effects.AddEffect(effect));
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        ObtainPowerup();
    }

}



