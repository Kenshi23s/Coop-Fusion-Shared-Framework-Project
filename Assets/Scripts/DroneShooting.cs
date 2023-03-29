using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;

[System.Serializable]
public struct BulletStats
{
   public int bulletdamage;
   public int bulletRadius;
}
public class DroneShooting : MonoBehaviour
{

    Camera cam;
    BulletStats bulletStats;
    Action onHit;
    Action onMiss;

    public DroneShooting(Camera cam, BulletStats bulletStats, Action onHit, Action onMiss)
    {
        this.cam = cam;
        this.bulletStats = bulletStats;
        this.onHit = onHit;
        this.onMiss = onMiss;
    }

    public void Shoot(Ray ray)
    {
        IDamagable target;
        if (DamagableWasHit(ray, out target))
        {
            target.TakeDamage(bulletStats.bulletdamage);
            onHit?.Invoke();
            return;
        }
        onMiss?.Invoke();
    }

    bool DamagableWasHit(Ray ray, out IDamagable damagable)
    {
        RaycastHit hit;

        if (Physics.SphereCast(ray, bulletStats.bulletRadius, out hit, Mathf.Infinity))
        {
            damagable = hit.transform.GetComponent<IDamagable>();

            if (damagable != null)
            {
                return true;
            }
        }

        damagable = null;
        return false;
    }
   
   
   
}
