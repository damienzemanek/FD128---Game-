using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;
using static SlotUI;
using static Effectability;

public class SlotManager : MonoBehaviour
{
    public DataSaver saver;
    public List<SlotUI> slots;
    public EffectUser equipEffect;

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

    public void EquippedAnItem(bool isDefaultItem)
    {
        if (isDefaultItem) return;
        equipEffect.UseEffect();
    }
}
