using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using System.Linq;

public class Powerup : MonoBehaviour
{
    EffectManager effects;
    [SerializeReference] Effect[] effect;

    private void Awake()
    {
        effects = EffectManager.Instance;
        effects.Ensure(this);
    }

    public void ObtainPowerup()
    {
        effect.ToList().ForEach(action: effect => effects.AddEffect(effect));
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        ObtainPowerup();
    }

}



