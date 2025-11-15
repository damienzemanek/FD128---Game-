using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns.CreationalPatterns; 

public class PlayerDataHolder : Singleton<PlayerDataHolder>
{
    [SerializeField] PlayerData _data;

    public PlayerData data { get => _data; set => _data = value; }
}
