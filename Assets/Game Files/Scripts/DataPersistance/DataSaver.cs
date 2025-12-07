using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DesignPatterns.CreationalPatterns;
using Extensions;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class DataSaver : DesignPatterns.CreationalPatterns.Singleton<DataSaver>   
{
    public GameExpData gameExpData;


    [Serializable]
    public class GameExpData
    {
        public int currentLevel;
        public int currentXP;
        public int pendingXP;
        public int unlocks;
        public int levelsUnlocked;
        public int[] levelHearts;


        public GameExpData()
        {
            currentLevel = 0;
            currentXP = 0;
            pendingXP = 0;
            unlocks = 0;
            levelHearts = new int[4];
        }
    }


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        LoadExp();
    }

    public void ResetProgress()
    {
        gameExpData = null;
        SaveExp();
    }

    public void SaveExp()
    {
        if (gameExpData == null) gameExpData = new GameExpData();
        string json = JsonUtility.ToJson(gameExpData);
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");
        File.WriteAllText(path, json);

        this.Log($"Saved Experience at '[{path}]' | Lvl: {gameExpData.currentLevel}, XP: {gameExpData.currentXP}");
    }

    public void LoadExp( )
    {
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");

        if (!File.Exists(path))
        {
            GameExpData newData = new GameExpData();
            gameExpData = newData;
            SaveExp();
            return;
        }

        var json = File.ReadAllText(path);
        var readData = JsonUtility.FromJson<GameExpData>(json);
        gameExpData = readData;

        this.Log($"Loaded Experience at '[{path}]' | Lvl: {gameExpData.currentLevel}, XP: {gameExpData.currentXP}");
    }

    public void OnApplicationQuit()
    {
        SaveExp();
    }
}
