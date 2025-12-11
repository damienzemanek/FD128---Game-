using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float currentTime;
    public CheckpointTime[] checkpoints;
    public TextMeshProUGUI TMP;

    [Serializable]
    public struct CheckpointTime
    {
        public bool hit;
        public float checkpointTime;
        public Action action;

        public void Hit()
        {
            hit = true;
            action?.Invoke();
        }
    }

    protected virtual void Start()
    {
        currentTime = 0;
    }

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;

        int minutes = (int)(currentTime / 60f);
        int seconds = (int)(currentTime % 60f);
        int millis = (int)((currentTime * 1000f) % 1000f);

        TMP.text = $"{minutes:00}:{seconds:00}";

        ChecktimeCheckpoints();

    }

    void ChecktimeCheckpoints()
    {
        for(int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i].hit) return;
            if (checkpoints[i].checkpointTime < currentTime) checkpoints[i].Hit();
        }
    }

}
