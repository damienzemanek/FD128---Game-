using System.Collections;
using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHp;
    [SerializeField, ReadOnly] float currentHp;

    private void OnEnable()
    {
        currentHp = maxHp;
    }

    public void TakeDmg(float amount)
    {
        this.Log("Ive been hit");
        currentHp -= amount;

        if (IsDead()) Die();
        else Hit();
    }

    void Hit()
    {

    }

    bool IsDead()
    {
        if (currentHp < 0) return true;
        return false;
    }

    void Die()
    {

    }
}
