using FacundoColomboMethods;
using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieManager : NetworkBehaviour, INetworkRunnerCallbacks
{
  
    public static ZombieManager instance;


    [SerializeField] SlimeNav model;
    

   [SerializeField,Tooltip("Solo lectura, no tocar")] 
   List<SlimeNav> slimeList = new List<SlimeNav>();

   [SerializeField] Transform[] spawns;

    public int slimesAlive => slimeList.Count;

    public Vector3 playerPos => GameManager.instance.model.transform.position;

    [SerializeField]
     int maxSlimes;  

    private void Awake()
    {      
        instance = this;
        spawns = ColomboMethods.GetChildrenComponents<Transform>(transform);
        GameManager.OnGameModeStart += SpawnSlime;
    }

    Vector3 NearestSpawn()
    {
        if (GameManager.instance.PlayerExists)
            return spawns[0].position;
        else
            return spawns[0].position;
            //return spawns.Skip(UnityEngine.Random.Range(0,spawns.Length-1)).First().position;     
    }

    public void SpawnSlime()
    {
        if (!Object || !Object.HasStateAuthority )
            return;
      
        Debug.Log("CallTrack");
        while (maxSlimes > slimesAlive)
        {
            SlimeNav Slime = BuildSlime();
            slimeList.Add(Slime);
            //Slime.transform.position = NearestSpawn();
        }
    }
    
    #region Pool
    SlimeNav BuildSlime() => Object.Runner.Spawn(model, NearestSpawn());   

    

    public void DespawnSlime(SlimeNav item) 
    {
        if (slimeList.Contains(item))
        {
            slimeList.Remove(item);
            Runner.Despawn(item.Object);
            SpawnSlime();
        } 

      

    }
    #region CallBacks
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player Join");
        SpawnSlime();
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

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
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
