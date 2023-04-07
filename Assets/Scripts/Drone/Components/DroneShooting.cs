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
        RaycastHit hit;
        
        if (Physics.SphereCast(ray, bulletStats.bulletRadius, out hit, Mathf.Infinity))
        {
            damagable = hit.transform.GetComponent<IDamagable>();
            //deberia tener al player en otra layer en vez de hacer esto cada vez q impacto un "Damagable"¿?
            if (damagable != null&& !hit.transform.TryGetComponent(out PlayerModel p))            
                return true;            
        }

        damagable = null;
        return false;
    }
   
   
   
}
