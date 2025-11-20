using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "ScriptableObjects/Player Data")]
public class PlayerData : ScriptableObject
{
    public float initialDmg;
    [ReadOnly] public float dmg;
    public int progressIncrease = 1;
    public int completeProgressValue = 4;
    public float maxVel = 10f;
    public int addToHeartCountIncrease = 1;

    [TitleGroup("Physics")]
    public float fallDrag;
    public float groundedDrag;

    private void OnEnable()
    {
        dmg = initialDmg;
    }
}
