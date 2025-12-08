using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using static Entity;

public static class Entity
{
    public static void Hit(this IHittable h, int _amount)
    {
        if (h.cannotHit) return;
        if (h.hittable.Hit(h)) h.hp -= _amount;
        if (h.hp <= 0)
        {
            h.OnDie();
            h.cannotHit = true;
        }
    }

    [Serializable]
    public struct Hittable
    {
        public bool isFleshy;
        public float invunrabilityTime;

        public bool Hit(IHittable h)
        {
            if (IsInvunrable(h)) return false;
            h.lastHitTime = Time.time;
            h.OnHit();
            this.Log("Hittable Hit");
            return true;
        }


        bool IsInvunrable(IHittable h)
        {
            return Time.time < h.lastHitTime + invunrabilityTime;
        }
    }
}
public interface IHittable
{
    public int hp { get; set; }
    public bool cannotHit { get; set; } 
    public float lastHitTime { get; set; }
    public Hittable hittable { get; set; }

    public abstract void OnHit();
    public abstract void OnDie();

}
