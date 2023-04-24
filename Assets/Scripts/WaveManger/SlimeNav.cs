using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
//RequireComponent[typeof(Rigidbody)]
[RequireComponent(typeof(NavMeshAgent))]
public class SlimeNav : Entity
{

    NavMeshAgent thisAgent;
    //Action<ZombieNav> ReturnMethod;
    [SerializeField] float explosionRadius, _hitRange;
    int _dmg;
    //HardPoint hp;
    
    

    //public void InitializeZombie(Action<ZombieNav> ReturnMethod) => this.ReturnMethod = ReturnMethod;

    private void Awake()
    {
        thisAgent = GetComponent<NavMeshAgent>();
    }
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();
        thisAgent.SetDestination(ZombieManager.instance.playerPos);
        if (Vector3.Distance(ZombieManager.instance.playerPos, transform.position) < _hitRange)
        {
            AOEdmg();
        }
    }

    //public void BackToPool() => ReturnMethod(this);

   


    void AOEdmg()
    {
        //manera correcta
        //IDamagable[] coliders = Physics.OverlapSphere(transform.position, explosionRadius).
        //    Where((x) => TryGetComponent(out IDamagable target))
        //   .Select(x => x.GetComponent<IDamagable>()).ToArray();
        // foreach (IDamagable item in coliders)       
        //    item.TakeDamage(_dmg);

        Debug.Log("AOE");
        //herejia, ilegible. Lo dejo por el meme nomas
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
        Object.Runner.Despawn(Object);
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
