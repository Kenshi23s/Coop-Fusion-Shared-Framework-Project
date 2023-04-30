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
    
    //private void Awake()
    //{
       

    //    Action Initialize = () =>
    //    {
    //        if (!ZombieManager.instance.hass) { Debug.Log("slime NO tiene autoridad"); return; }

    //        Debug.Log("slime tiene autoridad");

    //        if (TryGetComponent(out NavMeshAgent agent))
    //        {
    //            Destroy(agent);

    //        }
    //        thisAgent.updatePosition= false;
    //        thisAgent = gameObject.AddComponent<NavMeshAgent>();
    //        thisAgent.enabled = false;

    //        thisAgent.enabled = true;
    //        Update = SlimeBehaviour;
    //    };

    //    if (GameManager.instance.PlayerExists)
    //    {
    //        Initialize.Invoke();
    //    }
    //    else
    //    {
    //        GameManager.instance.OnPlayerSet += () => 
    //        {
    //            Initialize.Invoke();
    //        };
    //    }
    //}
    public override void Spawned()
    {
        base.Spawned();
        Action Initialize = () =>
        {
           

            if (TryGetComponent(out NavMeshAgent agent))
            {
                Destroy(agent);

            }
            thisAgent.updatePosition = false;
            thisAgent = gameObject.AddComponent<NavMeshAgent>();
            thisAgent.enabled = false;

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

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        Update?.Invoke();
    }

    void SlimeBehaviour()
    {      


        Debug.Log("Slime Movement Con State Authority");

        thisAgent.SetDestination(ZombieManager.instance.playerPos);  
        if (Vector3.Distance(ZombieManager.instance.playerPos, transform.position) < _hitRange) AOEdmg();

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
