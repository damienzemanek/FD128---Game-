using System.Collections;
using System.Collections.Generic;
using DesignPatterns.CreationalPatterns;
using Sirenix.OdinInspector;
using UnityEngine;

public class ProgressTracker : Singleton<ProgressTracker>
{
    PlayerDataHolder player;
    [SerializeField] int _currProgress;


    private void Start()
    {
        player = PlayerDataHolder.instance;
    }

    public void AddProgress(int amount)
    {
        _currProgress += amount;
        if (IsComplete()) CompleteGame();
    }

    bool IsComplete() => (_currProgress >= player.data.completeProgressValue);

    void CompleteGame()
    {

    }

}
