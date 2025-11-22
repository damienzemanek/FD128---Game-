using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public abstract class Effect 
{
    public string effectName;
    public bool permament = false;
    bool notPermament { get => !permament; }

    [ShowIf("notPermament")] public float currentDuration;
    public abstract void StartEffect(PlayerDataHolder _player);

    public abstract void StopEffect();
}

[Serializable]
public class IncreaseSpeed : Effect
{
    PlayerDataHolder player;
    [SerializeField] public float increaseAmount;


    public override void StartEffect(PlayerDataHolder _player)
    {
        player = _player;
        player.data.move.linearSpeedMult += increaseAmount;
    }

    public override void StopEffect()
    {
        player.data.move.linearSpeedMult -= increaseAmount;
    }
}

[Serializable]
public class IncreaseDmg : Effect
{
    PlayerDataHolder player;
     float dmg;
    [SerializeField] public float increaseAmount;


    public override void StartEffect(PlayerDataHolder _player)
    {
        player = _player;
        player.data.dmg += increaseAmount;
    }

    public override void StopEffect()
    {
        player.data.dmg -= increaseAmount;
    }
}




