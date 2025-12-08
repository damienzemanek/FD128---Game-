using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using static Extensions.PhysEX;

[CreateAssetMenu(fileName = "New Player Data", menuName = "ScriptableObjects/Player Data")]
public class PlayerData : ScriptableObject
{
    public int initialDmg;
    [ReadOnly] public int dmg;
    public float hitCooldown = 0.3f;


    [TitleGroup("Physics")]
    public float fallDrag;
    public float groundedDrag;
    public JumpSettings jump;
    public MoveSettings move;

    [TitleGroup("Progress")]
    public int progressIncrease = 1;
    public int completeProgressValue = 4;

    [TitleGroup("Experience")]
    public int experienceGainOnComplete;


    private void OnEnable()
    {
        dmg = initialDmg;
        move.linearSpeedMult = move.origLienarSpeedMult;
    }
}
