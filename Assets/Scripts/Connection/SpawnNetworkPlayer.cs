using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class SpawnNetworkPlayer : MonoBehaviour , INetworkRunnerCallbacks
{
    [SerializeField] NetworkPlayer _playerPrefab;
    [SerializeField] NetworkPlayer _dronePrefab;
    [SerializeField] GameObject _panel;

    Controller _inputHandler;
    NetworkRunner _currentRunner;
    
    public void OnConnectedToServer(NetworkRunner runner)
    {
        _currentRunner = runner;
        //Pregunta si nuestra topologia es shared, y si es, le pide al network runner que instancie en red  el prefab del player, en el spawn quq queramos.
       if(runner.Topology == SimulationConfig.Topologies.Shared)
        {
            Debug.Log("[Custom Msg] On Connected To Server - Spawning Player as Local...");

            //lo de runner.LocalPlayer es para decirle que el local player (el jugador que entró) va a tener la autoridad de mandarle inputs al prefab del player.

            _panel.gameObject.SetActive(true);
        }
    }

    //cada metodo se le asigna al boton correspondiente

    public void SpawnPlayer()
    {
        _currentRunner.Spawn(_playerPrefab, Vector3.zero, Quaternion.identity, _currentRunner.LocalPlayer);
        _panel.gameObject.SetActive(false);
    }

    public void SpawnDrone()
    {
        _currentRunner.Spawn(_dronePrefab, Vector3.zero, Quaternion.identity, _currentRunner.LocalPlayer);
        _panel.gameObject.SetActive(false);
    }



    //aca irian los inputs del jugador
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (!NetworkPlayer.Local) return;
        NetworkInputData data;

        if (!_inputHandler)
        {
            _inputHandler = NetworkPlayer.Local.GetComponent<Controller>();
        }
        else if(_inputHandler.ListenInputs(out data))
        {
            
            input.Set(data);
        }
    }

    #region CALLBACKS SIN USAR


    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        throw new NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        throw new NotImplementedException();
    }   

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        throw new NotImplementedException();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        throw new NotImplementedException();
    }

    #endregion
}
