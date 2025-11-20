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
    public float maxVel = 10f;

    [TitleGroup("Physics")]
    public float fallDrag;
    public float groundedDrag;


    [TitleGroup("Progress")]
    public int addToHeartCountIncrease = 1;
    public int completeProgressValue = 4;


    private void OnEnable()
    {
        dmg = initialDmg;
    }
}
