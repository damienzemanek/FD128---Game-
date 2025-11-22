using System;
using System.Collections;
using System.Collections.Generic;
using DesignPatterns.CreationalPatterns;
using Unity.VisualScripting;
using UnityEngine;

public class DataSaver : DesignPatterns.CreationalPatterns.Singleton<DataSaver>   
{

    [Serializable]
    public class GameExpData
    {
        public int currentLevel;
        public int currentXP;
    }

    public void SaveExp()
    {

    }

    public void LoadExp()
    {

    }
}
