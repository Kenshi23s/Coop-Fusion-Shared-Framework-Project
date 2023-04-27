using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.UI;

public class SpawnNetworkPlayer : MonoBehaviour , INetworkRunnerCallbacks
{
    [Header("Prefabs")]
    [SerializeField] NetworkPlayer _playerPrefab;
    [SerializeField] NetworkPlayer _dronePrefab;
  
    [Header("UI")]
    [SerializeField] GameObject _panel;
    [SerializeField] Text _loadingText;

   static Controller _inputHandler;
    NetworkRunner _currentRunner;

    public static Action OnGameModeStart;
    public static bool HasStarted; 


    private void Awake() => StartCoroutine(TextCoroutine());
  
    public void OnConnectedToServer(NetworkRunner runner)
    {
        _currentRunner = runner;
       
        //Pregunta si nuestra topologia es shared, y si es, le pide al network runner que instancie en red  el prefab del player, en el spawn quq queramos.
       if (runner.Topology == SimulationConfig.Topologies.Shared)
       {
            Debug.Log("[Custom Msg] On Connected To Server - Spawning Player as Local...");

            //lo de runner.LocalPlayer es para decirle que el local player (el jugador que entró) va a tener la autoridad de mandarle inputs al prefab del player.
            StopCoroutine(TextCoroutine());
            Destroy(_loadingText);
            _panel.gameObject.SetActive(true);
            
       }
    }
    IEnumerator TextCoroutine()
    {
        int count = 0;
    

        while (true)
        {
            yield return new WaitForSeconds(1f);
            count++;
            if (_loadingText!=null)
            {
                _loadingText.text += ".";
                if (count > 3)
                {
                    _loadingText.text = "Loading";
                    count = 0;
                }
            }
            else
            {
                break;
            }
            
        }
    }

    public void SpawnPlayer(bool arg)
    {
        if (arg)
        {
            _currentRunner.Spawn(_playerPrefab, Vector3.zero, Quaternion.identity, _currentRunner.LocalPlayer);
            CheckConnectedPlayers();
        }
        else if(!arg)
        {
            _currentRunner.Spawn(_dronePrefab, Vector3.zero, Quaternion.identity, _currentRunner.LocalPlayer);
            CheckConnectedPlayers();

        }

        Destroy(_panel.gameObject);
        //_panel.gameObject.SetActive(false);
    }

    void CheckConnectedPlayers()
    {
        if (_currentRunner.SessionInfo.PlayerCount > 1)
        {
            OnGameModeStart?.Invoke();
            HasStarted = true;
        }
    }
   

    public static void SetInputController(Controller _newController) => _inputHandler = _newController;
 

    //aca irian los inputs del jugador
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (!NetworkPlayer.Local) return;
        NetworkInputData data;

        if (!_inputHandler)
        {
            //_inputHandler = NetworkPlayer.Local.GetComponent<Controller>();
            Debug.LogError("no habia handler D:");
        }
        else if (_inputHandler.ListenInputs(out data))
        {
           
            input.Set(data);
        }
    }
    #region CALLBACKS SIN USAR


    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
      
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
      
    }   

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
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

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
      
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }

    #endregion
}
