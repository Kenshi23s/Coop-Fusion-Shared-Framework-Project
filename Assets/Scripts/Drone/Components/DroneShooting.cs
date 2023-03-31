using UnityEngine;
using System;

[System.Serializable]
public struct BulletStats
{
   public int bulletdamage;
   public int bulletRadius;
}
public class DroneShooting 
{

  
    BulletStats bulletStats;
    Action onHit;
    Action onMiss;

    public DroneShooting(Camera cam, BulletStats bulletStats, Action onHit, Action onMiss)
    {
        
        this.bulletStats = bulletStats;
        this.onHit = onHit;
        this.onMiss = onMiss;
    }

    public void Shoot()
    {
      
        IDamagable target;
        if (DamagableWasHit(Drone_CrossHair.instance.GetCrossHairScreenRay(), out target))
        {
            Debug.Log("ShootSucces");
            target.TakeDamage(bulletStats.bulletdamage);
            onHit?.Invoke();
            return;
        }
        Debug.Log("ShootNOTSucces");
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
