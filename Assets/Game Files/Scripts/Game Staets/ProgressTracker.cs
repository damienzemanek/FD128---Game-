using System.Collections;
using System.Collections.Generic;
using DesignPatterns.CreationalPatterns;
using Sirenix.OdinInspector;
using UnityEngine;
using Extensions;
using static Extensions.FadeEX;

public class ProgressTracker : Singleton<ProgressTracker>
{
    PlayerDataHolder player;
    DataSaver saver;
    [SerializeField] GameObject completeGameDisplay;
    [SerializeField] EntityMove move;
    [SerializeField] Look look;
    [SerializeField] int _currProgress;
    [ShowInInspector, ReadOnly] int completeAmount { get => player ? player.data.completeProgressValue : 0; }



    private void Start()
    {
        player = PlayerDataHolder.instance;
        saver = DataSaver.instance;
    }

    public void AddProgress(int amount)
    {
        _currProgress += amount;
        if (IsComplete()) CompleteGame();
    }

    bool IsComplete() => (_currProgress >= player.data.completeProgressValue);

    [Button]
    void CompleteGame()
    {
        look.ToggleUpdateMouseLooking(false);
        move.canMove = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        completeGameDisplay.SetActive(true);
        saver.gameExpData.pendingXP += player.data.experienceGainOnComplete;
    }

}
