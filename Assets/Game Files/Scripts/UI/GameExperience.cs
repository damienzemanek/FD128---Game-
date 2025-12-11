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
using static Extensions.AnimEX;
using static Effectability;

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

    [TabGroup("Effects")] public EffectUser[] fx_levelup;

    [TabGroup("Anims")] [SerializeField] Animatable xpBarAnims;
    [TabGroup("Anims")][SerializeField] string anim_levelingupinprog = "levelingupinprog";
    [TabGroup("Anims")][SerializeField] string anim_levelup = "levelup";
    [TabGroup("Anims")][SerializeField] string anim_idle = "idle";
    [TabGroup("Anims")][SerializeField] bool isLeveledUpAnimPlaying;

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
        this.Log("Gainign xp");
        Level lvl = levels[currentLevel];

        float reaminingXpToLevelUp = lvl.xpToLevelUp - lvl.currentXP;
        float xpUsedForThisLevel = Mathf.Min(amount, reaminingXpToLevelUp);
        float xpGained = xpUsedForThisLevel / lvl.xpToLevelUp;
        float leftOver = lvl.GainXPreturnLeftover(amount);
        bool didLevelUp = (xpUsedForThisLevel == reaminingXpToLevelUp);

        xpBarAnims.Animate(anim_levelingupinprog);
        xpSlider.GainValue(xpGained, () =>
        {
            StartCoroutine(ThisLevelGainComplete(leftOver, didLevelUp));
        });

    }

    IEnumerator ThisLevelGainComplete(float leftOver, bool didLevelUp)
    {
        DisplayCurrentExperience();

        if (currentLevel == levels.Count - 1)
            if (levels[currentLevel].complete)
            {
                LevelUpPlayer();
                yield break;
            }

        if (leftOver > 0 && currentLevel <= levels.Count - 1 && didLevelUp)
        {
            LevelUpPlayer();
            yield return new WaitUntil(() => isLeveledUpAnimPlaying == false);
            GainExperience(leftOver);
        }
        else if(leftOver == 0 && didLevelUp)
        {
            LevelUpPlayer();
            yield return new WaitUntil(() => isLeveledUpAnimPlaying == false);
        }
        else
        {
            xpBarAnims.CrossFade(anim_idle);
            isLeveledUpAnimPlaying = false;
        }


        this.Log("stopped gaining xp");
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
        isLeveledUpAnimPlaying = true;
        source.Play(levelupSound);
        this.DelayedCall(() => xpBarAnims.Animate(anim_levelup, this, () => isLeveledUpAnimPlaying = false), 0.1f);
        fx_levelup.UseEffectsAll();
        this.Log("leveled up");
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

    public float GainXPreturnLeftover(float amount)
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
