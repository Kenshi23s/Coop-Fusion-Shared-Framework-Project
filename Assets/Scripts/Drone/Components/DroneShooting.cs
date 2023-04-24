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
    Drone_CrossHair crossHair;

    public DroneShooting(Camera cam, BulletStats bulletStats, Action onHit, Action onMiss, Drone_CrossHair crossHair)
    {
        this.crossHair = crossHair;
        this.bulletStats = bulletStats;
        this.onHit = onHit;
        this.onMiss = onMiss;
        //Debug.Log(this.crossHair);
    }

    public void Shoot()
    {
      
        IDamagable target;
        if (DamagableWasHit(crossHair.GetCrossHairScreenRay(), out target))
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
        //RaycastHit[] OnSight = Physics.SphereCastAll(ray, bulletStats.bulletRadius, out RaycastHit hit, Mathf.Infinity);
        RaycastHit[] OnSight = Physics.SphereCastAll(ray, bulletStats.bulletRadius);
      
        if (OnSight.Length > 0)
        {
            
            foreach (RaycastHit item in OnSight)
            {
                if (item.transform.TryGetComponent(out IDamagable target))
                {
                    Debug.Log(target);
                    damagable = target;
                    return true;
                }
            }
        }
      

        damagable = null;
        return false;
    }
   
   
   
}
