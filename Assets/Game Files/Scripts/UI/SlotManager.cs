using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;
using static SlotUI;

public class SlotManager : MonoBehaviour
{
    public DataSaver saver;
    public List<SlotUI> slots;

    private void Awake()
    {
        saver = DataSaver.Instance;
    }

    private void Start()
    {
        for(int i = 0; i < saver.gameExpData.unlocks;  i++)
            slots[index: i].Unlock();

    }

    public void Unlock(Unlocks unlock)
    {
        this.Log("Unlocking something");
        SlotUI slot = slots.FirstOrDefault(slot => slot.unlock == unlock);
        slot.Unlock();
    }

    public void UnEquipAll()
    {
        slots.ForEach(slot => slot.wearableObj.SetActive(false));
    }
}
