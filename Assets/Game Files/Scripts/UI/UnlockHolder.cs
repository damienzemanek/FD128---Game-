using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockHolder : MonoBehaviour
{
    public GameExperience gameXP;
    public SlotManager slotManager;
    public SlotUI.Unlocks unlock;

    public void Unlock()
    {
        slotManager.Unlock(unlock);
        gameXP.saver.gameExpData.unlocks++;
        gameXP.saver.SaveExp();
    }
       
}
