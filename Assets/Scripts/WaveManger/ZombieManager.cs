using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FacundoColomboMethods;
using Fusion;

public class ZombieManager : NetworkBehaviour
{
  
    public static ZombieManager instance;
    

    [SerializeField] SlimeNav model;
    //PoolObject<ZombieNav> zombiePool = new PoolObject<ZombieNav>();

   [SerializeField,Tooltip("Solo lectura, no tocar")] 
   List<SlimeNav> zombiesList = new List<SlimeNav>();

    Transform[] spawns;

    public int zombiesAlive => zombiesList.Count;
    //public float zombieDamage;

    [SerializeField]
     int maxZombies;
    

    //[SerializeField] PlayerEntity Player;
    public Vector3 playerPos => GameManager.instance.model.transform.position;

    private void Start()
    {
      
        instance = this;
        spawns = ColomboMethods.GetChildrenComponents<Transform>(transform);
        //zombiePool.Intialize(TurnOnZombie, TurnOffZombie, BuildZombie);
        Debug.Log("Runner");
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
            SlimeNav Slime = BuildSlime();
            zombiesList.Add(Slime);
            Slime.transform.position = NearestSpawn();
        }
    }
    
    #region Pool
    void TurnOnZombie(SlimeNav z) => z.gameObject.SetActive(true);


    void TurnOffZombie(SlimeNav z)
    {
        //z.des
        //zombiesList.Remove(z);
        //z.gameObject.SetActive(false);
        SpawnZombie();
    }

    SlimeNav BuildSlime()
    {
        Debug.Log(model);
        Debug.Log(Runner);
        return Runner.Spawn(model);
    } 
    //{
    //    ZombieNav zombie = Runner.Spawn(model);
    //    //zombie.InitializeZombie(ReturnZombie);
    //    return zombie;
    //}

    //void ReturnZombie(ZombieNav z) => zombiePool.Return(z);

    //ZombieNav GetEnemy() =>  zombiePool.Get();
    
    
     
    #endregion
}
