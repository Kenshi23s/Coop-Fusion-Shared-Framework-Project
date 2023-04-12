using FacundoColomboMethods;
using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : NetworkBehaviour, INetworkRunnerCallbacks
{
  
    public static ZombieManager instance;

    NetworkRunner myRunner;

    [SerializeField] SlimeNav model;
    //PoolObject<ZombieNav> zombiePool = new PoolObject<ZombieNav>();

   [SerializeField,Tooltip("Solo lectura, no tocar")] 
   List<SlimeNav> slimeList = new List<SlimeNav>();

    Transform[] spawns;

    public int zombiesAlive => slimeList.Count;
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
        //SpawnZombie();
    }

    Vector3 NearestSpawn() => ColomboMethods.GetNearest(spawns, playerPos).position;

    public void SpawnSlime()
    {
        if (!(myRunner.Topology == SimulationConfig.Topologies.Shared))
            return;

        //for (int i = zombiesAlive; i < maxZombies; i++)
        //{
        //    ZombieNav zombie = BuildZombie();

        //    zombiesList.Add(zombie);
        //    zombie.transform.position = NearestSpawn();
        //}

        while (zombiesAlive <= maxZombies)
        {
            SlimeNav Slime = BuildSlime();
            slimeList.Add(Slime);
            Slime.transform.position = NearestSpawn();
            Debug.Log("SpawnSlime");
        }
    }
    
    #region Pool
    void TurnOnZombie(SlimeNav z) => z.gameObject.SetActive(true);


    void TurnOffZombie(SlimeNav z)
    {
        //z.des
        //zombiesList.Remove(z);
        //z.gameObject.SetActive(false);
        SpawnSlime();
    }

    SlimeNav BuildSlime()
    {
        Debug.Log(model);
        Debug.Log(myRunner);
        return myRunner.Spawn(model);
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        if (myRunner==null)
        {
            myRunner = runner;
            Debug.Log(myRunner);
        }
       
        SpawnSlime();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
       
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
       
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
       
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
      
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
       
    }

    

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
       
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
       
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
       
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
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
