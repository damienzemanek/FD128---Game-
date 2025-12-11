using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Extensions.AnimEX;

public class BunnyEat : MonoBehaviour
{
    public Animatable bunnyAnims;
    public string anim_eatDown;
    public string anim_eatUp;

    public enum EatZone
    {
        Up,
        Down,
        None
    }
    public EatZone currentZone;
    public bool eating;

    public void StartEating(EatZone zone)
    {
        currentZone = zone;
        eating = true;
        EatAnims(zone);
    }

    public void StopEating(EatZone zone)
    {
        if (zone != currentZone) return;
        currentZone = EatZone.None;
        eating = false;
    }

    void EatAnims(EatZone zone)
    {
        switch(zone)
        {
            case EatZone.Up:   bunnyAnims.CrossFade(anim_eatUp); break;
            case EatZone.Down: bunnyAnims.CrossFade(anim_eatDown); break;
        }
    }
}
