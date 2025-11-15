using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameExperience : MonoBehaviour
{
    public int currentLevel;
    [SerializeReference] public List<Level> levels;
    [SerializeField] SliderRuntime xpSlider;
    [SerializeField] public TextMeshProUGUI levelNumberText;

    private void OnEnable()
    {
        levels.ForEach(l => l.game = this);
        DisplayCurrentExperience();
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
    }
}
