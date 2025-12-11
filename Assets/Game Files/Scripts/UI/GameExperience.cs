using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Extensions;
using static Extensions.AudioEX;

public class GameExperience : MonoBehaviour
{
    [ShowInInspector, ReadOnly] public DataSaver saver;
    public int currentLevel { get => saver ? saver.gameExpData.currentLevel : 0; set => saver.gameExpData.currentLevel = value; }
    public float currentXP { get => saver ? saver.gameExpData.currentXP : 0; set => saver.gameExpData.currentXP = (int)value; }

    [SerializeReference] public List<Level> levels;
    [SerializeField] SliderRuntime xpSlider;
    [SerializeField] public TextMeshProUGUI levelNumberText;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip levelupSound;

    private void Awake()
    {
        saver = DataSaver.Instance;
    }

    private void OnEnable()
    {
        levels.ForEach(l => l.game = this);
    }
    private void Start()
    {
        levels[currentLevel].currentXP = saver.gameExpData.currentXP;
        DisplayCurrentExperience();

        if (saver.gameExpData.pendingXP > 0)
        {
            GainExperience(saver.gameExpData.pendingXP);
            saver.gameExpData.pendingXP = 0;
        }
    }

    public void DisplayCurrentExperience()
    {
        xpSlider.Display(levels[currentLevel].sliderVal);
        DisplayLevelText();
    }


    [Button]
    public void GainExperience(float amount)
    {
        Level lvl = levels[currentLevel];

        float reaminingXpToLevelUp = lvl.xpToLevelUp - lvl.currentXP;
        float xpUsedForThisLevel = Mathf.Min(amount, reaminingXpToLevelUp);
        float xpGained = xpUsedForThisLevel / lvl.xpToLevelUp;
        float leftOver = lvl.GainXP(amount);

        xpSlider.GainValue(xpGained, () => ThisLevelGainComplete(leftOver));

    }

    void ThisLevelGainComplete(float leftOver)
    {
        DisplayCurrentExperience();

        if (currentLevel == levels.Count - 1)
            if (levels[currentLevel].complete) return;

        if (leftOver > 0 && currentLevel <= levels.Count - 1)
            GainExperience(leftOver);
    }

    public void DisplayLevelText()
    {
        levelNumberText.text = $"Lvl: {currentLevel}";
    }

    private void OnDisable()
    {
        saver.gameExpData.currentLevel = currentLevel;
        saver.gameExpData.currentXP = (int)levels[currentLevel].currentXP;

        if (saver != null)
            saver.SaveExp();
        else this.Warn("No saver present");
    }

    public void LevelUpPlayer()
    {
        source.Play(levelupSound);
    }
}

[Serializable]
public class Level
{
    [SerializeField] public bool complete;
    [ReadOnly] public GameExperience game;
    public float xpToLevelUp;
    [ReadOnly] public float currentXP;
    public UnityEvent levelUpHook;

    public float sliderVal { get => currentXP / xpToLevelUp; }

    public Level()
    {
        currentXP = 0;
    }

    public float GainXP(float amount)
    {
        currentXP += amount;
        if (currentXP >= xpToLevelUp)
        {
            float leftOver = currentXP - xpToLevelUp;
            LevelUp();

            return leftOver;
        }
        return 0;
    }

    public void LevelUp()
    {
        if (complete) return;
        complete = true;

        if (game.currentLevel < game.levels.Count - 1)
            game.currentLevel++;

        levelUpHook?.Invoke();
        this.Log("Leveled up");
    }
}
