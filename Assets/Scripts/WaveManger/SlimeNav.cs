using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SlimeNav : Entity
{

    NavMeshAgent thisAgent;
   
    [SerializeField] float explosionRadius, _hitRange;
    [SerializeField] ParticleSystem _shootPS;
    [SerializeField] Transform _PSTransform;
    int _dmg;
    



 
    Action Update;
    
 
    public override void Spawned()
    {
        if (!Object.HasStateAuthority) return;
              
        base.Spawned();
        Action Initialize = () =>
        {
         
            Update = SlimeBehaviour;
            damageCallBack += PlayParticleOnDamage;
        };

        if (GameManager.instance.PlayerExists) Initialize.Invoke();

        else GameManager.instance.OnPlayerSet += () =>
        {
            Initialize.Invoke();
        };



    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();
        Update?.Invoke();
    }

    void SlimeBehaviour()
    {      
        Debug.Log("Slime Movement Con State Authority");

        Vector3 dir = ZombieManager.instance.playerPos - transform.position;
        transform.position += dir.normalized * Time.fixedDeltaTime * 5f;
        if (dir.magnitude< _hitRange) TakeDamage(10000);

    }

    void PlayParticleOnDamage()
    {
        Instantiate(_shootPS, _PSTransform.position, Quaternion.identity);
    }
   

    void AOEdmg()
    {
     
        IDamagable[] targets = Physics.OverlapSphere(transform.position, explosionRadius).
        Where((x) => TryGetComponent(out IDamagable target)).
        Select(x => x.GetComponent<IDamagable>()).ToArray();

        if (targets.Length > 0)
        {
            foreach (var item in targets)
            {
                Debug.Log(item);
                if (item != null && GetHashCode() != item.GetHashCode() )
                {
                    item.TakeDamage(_dmg);
                }             
            }
        }
        Debug.Log("Mori");
        ZombieManager.instance.DespawnSlime(this);
       
    }


    protected override void Die() => AOEdmg();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _hitRange);
    }
}
