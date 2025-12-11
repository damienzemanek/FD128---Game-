using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsTimer : Timer
{
    PlayerDataHolder player;
    [SerializeField] public int currentHearts;

    private void Awake()
    {
        player = PlayerDataHolder.Instance;
    }

    protected override void Start()
    {
        currentHearts = 3;
        base.Start();
        checkpoints[0].action = () => { currentHearts = 2; };
        checkpoints[1].action = () => { currentHearts = 1; };
        checkpoints[2].action = () => { currentHearts = 0; };
    }

}
