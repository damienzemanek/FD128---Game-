using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "ScriptableObjects/Player Data")]
public class PlayerData : ScriptableObject
{
    public float dmg;
    public float maxVel = 10f;
    public int addToHeartCountIncrease = 1;
}
