using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotUI : MonoBehaviour
{
    public enum Unlocks
    {
        Wizard,
        Helmet,
        Horns
    }

    public Unlocks unlock;
    public bool unlocked = false;
    public GameObject lockObj;


    public void Unlock()
    {
        unlocked = true;
        lockObj.SetActive(false);
    }

}
