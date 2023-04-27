using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
//RequireComponent[typeof(Rigidbody)]
//[RequireComponent(typeof(NavMeshAgent))]
public class SlimeNav : Entity
{

    NavMeshAgent thisAgent;
    //Action<ZombieNav> ReturnMethod;
    [SerializeField] float explosionRadius, _hitRange;
    int _dmg;
    //HardPoint hp;



    //public void InitializeZombie(Action<ZombieNav> ReturnMethod) => this.ReturnMethod = ReturnMethod;
    Action Update;
    
    private void Awake()
    {
       
        if (TryGetComponent(out NavMeshAgent agent))
        {
            Destroy(agent);
            
        }
        thisAgent = gameObject.AddComponent<NavMeshAgent>();
        thisAgent.enabled = false;

        Action Initialize = () =>
        {
            thisAgent.enabled = true;
            Update = SlimeBehaviour;
        };
        if (GameManager.instance.PlayerExists)
        {
            Initialize.Invoke();
        }
        else
        {
            GameManager.instance.OnPlayerSet += () => 
            {
                Initialize.Invoke();
            };
        }
    }
  
    void SlimeBehaviour()
    {
        thisAgent.SetDestination(ZombieManager.instance.playerPos);
        if (Vector3.Distance(ZombieManager.instance.playerPos, transform.position) < _hitRange)
        {
            AOEdmg();
        }
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        Update?.Invoke();
    }

    void AOEdmg()
    {
     
        IDamagable[] targets = Physics.OverlapSphere(transform.position, explosionRadius).
        Where((x) => TryGetComponent(out IDamagable target)).
        Select(x => x.GetComponent<IDamagable>()).ToArray();
        if (targets.Length>0)
        {
            foreach (var item in targets)
            {
                item.TakeDamage(_dmg);
            }
        }       
     
        Die();
    }


    protected override void Die()
    {
        Debug.Log("Die");
        if (Object.HasStateAuthority) 
        {
            ZombieManager.instance.SpawnSlime();
            Object.Runner.Despawn(Object); 
        } 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
