using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DesignPatterns.CreationalPatterns;
using Sirenix.OdinInspector;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class EffectManager : Singleton<EffectManager>
{
    [SerializeField] PlayerDataHolder playerDataHolder;
    [SerializeReference, ReadOnly] public List<Effect> effects = new List<Effect>();

    protected override void Awake()
    {
        base.Awake();
        playerDataHolder = PlayerDataHolder.Instance;
    }

    private void FixedUpdate()
    {
        EffectInUse();
    }

    public void AddEffect(Effect effect)
    {
        if (effects.Count > 0)
        {
            Effect match = effects.FirstOrDefault(e => e.GetType() == effect.GetType());
            if (match == null) { AddAndStartEffect(effect); return; }

            match.currentDuration += effect.currentDuration;
        }
        else
            AddAndStartEffect(effect);
    }

    void AddAndStartEffect(Effect effect)
    {
       effects.Add(effect);
       effect.StartEffect(playerDataHolder);
    }


    void EffectInUse()
    {
        if (effects.Count <= 0) return;

        for(int i = effects.Count - 1; i >= 0; i--)
        {
            Effect effect = effects[i];
            effect.currentDuration -= Time.deltaTime;

            if(effect.currentDuration <= 0)
            {
                effect.StopEffect();
                effects.RemoveAt(i);
            }
        }
    }
}
