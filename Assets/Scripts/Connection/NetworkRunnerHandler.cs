using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NetworkRunner))]

public class NetworkRunnerHandler : NetworkBehaviour
{
    NetworkRunner _networkRunner;
    [SerializeField] Scene _scene;

    private void Awake()
    {
        _networkRunner = GetComponent<NetworkRunner>();
        
        //El network runner inicializa una sala, en la que le especificamos que va a ser de tipo Shared y qué escena queremos cargar
        var clientTask = InitializeNetworkRunner(_networkRunner, GameMode.Shared, SceneManager.GetActiveScene().buildIndex);
           

    }
     
    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gamemode, SceneRef scene)
    {
        //A traves del scene manager que tiene fusion, pregunta internamente si estamos en la misma escena, lo cargo, sino no.

        var sceneObject = runner.GetComponent<NetworkSceneManagerDefault>();

         return runner.StartGame(new StartGameArgs
         {
            GameMode = gamemode,
            Scene = scene,
            SessionName = "GameSession",
            SceneManager = sceneObject
        });
    }
}
