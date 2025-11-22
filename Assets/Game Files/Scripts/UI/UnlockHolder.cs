using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockHolder : MonoBehaviour
{
    public SlotManager slotManager;
    public SlotUI.Unlocks unlock;

    public void Unlock() =>
        slotManager.Unlock(unlock);
}
