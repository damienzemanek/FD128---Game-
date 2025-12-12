using System.Collections;
using System.Collections.Generic;
using DesignPatterns.CreationalPatterns;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Extensions;
using static Extensions.FadeEX;
using static Extensions.AudioEX;

public class ProgressTracker : Singleton<ProgressTracker>
{
    PlayerDataHolder player;
    DataSaver saver;
    [SerializeField] GameObject completeGameDisplay;
    [SerializeField] EntityMove move;
    [SerializeField] Look look;
    [SerializeField] HeartsTimer timer;
    [SerializeField] int _currProgress;
    [ShowInInspector, ReadOnly] int completeAmount { get => player ? player.data.completeProgressValue : 0; }
    [SerializeField] public Image[] heartImages;
    [SerializeField] public Sprite fullHeart;
    [SerializeField] AFX_Single afx_lvlComplete;


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
        afx_lvlComplete.Play();
        look.ToggleUpdateMouseLooking(false);
        move.canMove = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        completeGameDisplay.SetActive(true);
        saver.gameExpData.pendingXP += player.data.experienceGainOnComplete;
        if(saver.gameExpData.levelHearts[player.currentLevel] < timer.currentHearts)
            saver.gameExpData.levelHearts[player.currentLevel] = timer.currentHearts;
        saver.SaveExp();
        StartCoroutine(DisplayHearts());
    }

    IEnumerator DisplayHearts()
    {
        for(int i = 0; i < timer.currentHearts; i++)
        {
            yield return new WaitForSeconds(0.4f);
            heartImages[i].sprite = fullHeart;
            yield return new WaitForSeconds(0.5f);
        }
    }

}
