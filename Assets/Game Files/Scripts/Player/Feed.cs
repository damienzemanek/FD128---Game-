using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using DesignPatterns.CreationalPatterns;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class Feed : Singleton<Feed>
{
    [Inject] EntityControls controls;
    [SerializeField] bool _canFeed;
    [SerializeField] GameObject feedDisplay;

    [Title("Anims")][SerializeField] Animator animator;

    [Title("Feeding")][SerializeField, ReadOnly] FeedTrigger goreImEating;
    [Title("Feeding")][SerializeField, ReadOnly] float currentFeed = 0f;
    [Title("Feeding")][SerializeField] float feedIncr = 0.1f;
    [Title("Feeding")][SerializeField] float feedToBeFull;



    public bool canFeed { get => _canFeed; set => _canFeed = value; }

    private void OnEnable()
    {
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
        Destroy(goreImEating.parent);
        StopDisplay();
        FeedStop();
        currentFeed = 0;
    }


}
