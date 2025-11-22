using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SlotUI;

public class SlotManager : MonoBehaviour
{
    public List<SlotUI> slots;

    public void Unlock(Unlocks unlock)
    {
        SlotUI slot = slots.FirstOrDefault(slot => slot.unlock == unlock);
        slot.Unlock();
    }
}
