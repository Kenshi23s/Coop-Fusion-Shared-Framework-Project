using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NetworkRunner))]

public class NetworkRunnerHandler : MonoBehaviour
{
    NetworkRunner _networkRunner;
    [SerializeField] Scene _scene;

    private void Start()
    {
        _networkRunner = GetComponent<NetworkRunner>();

        var clientTask = InitializeNetworkRunner(_networkRunner, GameMode.Shared, SceneManager.GetActiveScene().buildIndex);
           

    }
     
    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gamemode, SceneRef scene)
    {
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
