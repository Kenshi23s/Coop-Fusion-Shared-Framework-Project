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
    void Update()
    {
        thisAgent.SetDestination(ZombieManager.instance.playerPos);
        float distance = (ZombieManager.instance.playerPos - transform.position).magnitude;
        if (distance <= _hitRange)
        {
            //logica golpe
        }
    }

    //public void BackToPool() => ReturnMethod(this);

   

    protected override void Die()
    {


        Runner.Despawn(Object);
            




    }

    void AOEdmg()
    {
        //manera correcta
        //IDamagable[] coliders = Physics.OverlapSphere(transform.position, explosionRadius).
        //    Where((x) => TryGetComponent(out IDamagable target))
        //   .Select(x => x.GetComponent<IDamagable>()).ToArray();
        // foreach (IDamagable item in coliders)       
        //    item.TakeDamage(_dmg);


        //herejia, ilegible. Lo dejo por el meme nomas
        foreach (IDamagable item in Physics.OverlapSphere(transform.position, explosionRadius).
        Where((x) => TryGetComponent(out IDamagable target)).
        Select(x => x.GetComponent<IDamagable>()))
        {
            item.TakeDamage(_dmg);

        }

        Die();
    }
    //public void SetHardpoint(HardPoint hp)=> this.hp = hp;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
