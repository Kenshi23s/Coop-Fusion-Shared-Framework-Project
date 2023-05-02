using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : NetworkBehaviour,IDamagable
{
    [SerializeField]int life;
    [SerializeField]int maxLife;
    public event Action damageCallBack;

    public void TakeDamage(int dmg)
    {  
        RPC_TakeDamage(dmg);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    void RPC_TakeDamage(int dmg)
    {
        damageCallBack?.Invoke();
        if (!Object.HasStateAuthority) return;

        life -= dmg;
        Debug.Log(this.gameObject.name + " " + life);
        if (life < 0)
        {
            Die();
        }
    }

    protected abstract void Die();

}
