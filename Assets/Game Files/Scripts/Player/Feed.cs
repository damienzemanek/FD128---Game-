using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using DesignPatterns.CreationalPatterns;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

[DefaultExecutionOrder(1)]
public class Feed : Singleton<Feed>
{
    [Inject] EntityControls controls;
    PlayerDataHolder player;
    [SerializeField] bool _canFeed;

    [Title("Anims")][SerializeField] Animator animator;

    [Title("Refs")][SerializeField] public Attack attack;
    [SerializeField] GameObject feedDisplay;
    [SerializeField] TextMeshProUGUI heartCountText;
    [SerializeField] UIJitter heartImageJitter;
    [SerializeField] GameObject heartsEatenlabel;


    [Title("Feeding")][SerializeField, ReadOnly] FeedTrigger goreImEating;
    [SerializeField, ReadOnly] float currentFeed = 0f;
    [SerializeField] float feedIncr = 0.1f;
    [SerializeField] float feedToBeFull;

    [Title("Hearts")][SerializeField] int currentHeartCount = 0;



    public bool canFeed { get => _canFeed; set => _canFeed = value; }

    protected override void Awake()
    {
        base.Awake();
        player = PlayerDataHolder.Instance;

        attack.Ensure(this);
        feedDisplay.Ensure(this);
        player.Ensure(this);
    }

    private void OnEnable()
    {
        currentHeartCount = 0;
        heartCountText.text = "" + currentHeartCount;
        heartsEatenlabel.SetActive(false);

        goreImEating = null;
        currentFeed = 0f;
        controls.interactHold += FeedStart;
        controls.interactHoldCancel += FeedStop;
    }

    private void OnDisable()
    {
        controls.interactHold -= FeedStart;
        controls.interactHoldCancel -= FeedStop;
    }


    private void Start()
    {
        feedDisplay.SetActive(false);
    }


    public void Display(FeedTrigger gorePile)
    {
        goreImEating = gorePile;
        feedDisplay.SetActive(true);
        canFeed = true;
    }

    public void StopDisplay()
    {
        goreImEating = null;
        feedDisplay.SetActive(false);
        canFeed = false;
    }

    public void FeedStart()
    {
        if (!canFeed) return;

        print("feeding");
        currentFeed += 0.1f;
        animator.SetBool("eating", true);

        if (currentFeed > feedToBeFull) Consume();
    }


    public void FeedStop()
    {
        print("feeding stop");
        animator.SetBool("eating", false);
    }

    public void Consume()
    {
        if (goreImEating == null) { this.Log("EARLY RETURN: goreImeating null"); return; }
        attack.DisableBloodyHands();
        Destroy(goreImEating.parent);
        StopDisplay();
        FeedStop();
        AddToHeartCount();
        currentFeed = 0;
        ProgressTracker.instance.AddProgress(player.data.progressIncrease);
    }

    void AddToHeartCount()
    {
        currentHeartCount += player.data.addToHeartCountIncrease;
        heartCountText.text = "" + currentHeartCount;
        heartImageJitter.JitterThenReset();
        heartsEatenlabel.SetActive(true);
    }


}
