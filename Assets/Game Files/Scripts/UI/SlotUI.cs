using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

public class SlotUI : MonoBehaviour
{
    public SlotManager slotManager;
    public enum Unlocks
    {
        None,
        Wizard,
        Helmet,
        Horns
    }

    bool notNone => unlock != Unlocks.None;

    public Unlocks unlock;
    public bool unlocked = false;
    public GameObject lockObj;
    [ShowIf("notNone")] public GameObject wearableObj;

    private void Start()
    {
        if (!notNone)
        {
            lockObj.SetActive(false);
            unlocked = true;
        }
    }


    public void Unlock()
    {
        unlocked = true;
        lockObj.SetActive(false);
        this.Log($"unlocked: {unlock.ToString()}");
    }

    public void Equip()
    {
        slotManager.UnEquipAll();
        if(notNone) wearableObj.SetActive(true);
        slotManager.EquippedAnItem(!notNone);
    }

}
