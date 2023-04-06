using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FacundoColomboMethods;
using Fusion;

public class ZombieManager : NetworkBehaviour
{
  
    public static ZombieManager instance;

    [SerializeField] ZombieNav model;
    //PoolObject<ZombieNav> zombiePool = new PoolObject<ZombieNav>();

    List<ZombieNav> zombiesList = new List<ZombieNav>();

    Transform[] spawns;

    public int zombiesAlive => zombiesList.Count;
    //public float zombieDamage;

    [SerializeField]
     int maxZombies;
    

    //[SerializeField] PlayerEntity Player;
    public Vector3 playerPos => GameManager.instance.model.transform.position;

    private void Awake()
    {
      
        instance = this;
        spawns = ColomboMethods.GetChildrenComponents<Transform>(transform);
        //zombiePool.Intialize(TurnOnZombie, TurnOffZombie, BuildZombie);
        SpawnZombie();
    }

    Vector3 NearestSpawn() => ColomboMethods.GetNearest(spawns, playerPos).position;

    public void SpawnZombie()
    {

        //for (int i = zombiesAlive; i < maxZombies; i++)
        //{
        //    ZombieNav zombie = BuildZombie();

        //    zombiesList.Add(zombie);
        //    zombie.transform.position = NearestSpawn();
        //}

        while (zombiesAlive <= maxZombies)
        {
            ZombieNav zombie = BuildZombie();
            zombiesList.Add(zombie);
            zombie.transform.position = NearestSpawn();
        }
    }
    
    #region Pool
    void TurnOnZombie(ZombieNav z) => z.gameObject.SetActive(true);


    void TurnOffZombie(ZombieNav z)
    {
        //z.des
        //zombiesList.Remove(z);
        //z.gameObject.SetActive(false);
        SpawnZombie();
    }

    ZombieNav BuildZombie() => Runner.Spawn(model);
    //{
    //    ZombieNav zombie = Runner.Spawn(model);
    //    //zombie.InitializeZombie(ReturnZombie);
    //    return zombie;
    //}

    //void ReturnZombie(ZombieNav z) => zombiePool.Return(z);

    //ZombieNav GetEnemy() =>  zombiePool.Get();
    
    
     
    #endregion
}
