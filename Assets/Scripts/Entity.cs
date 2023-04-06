using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : NetworkBehaviour,IDamagable
{
    int life;
    int maxLife;
    public event Action<int> damageCallBack;

    public void TakeDamage(int dmg)
    {
        life -= dmg;
        if (life<=0)
        {
            Die();
        }
    }

    protected abstract void Die();

}
