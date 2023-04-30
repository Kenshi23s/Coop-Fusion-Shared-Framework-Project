using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Fusion;
using UnityEngine.UIElements;

public class GameManager : NetworkBehaviour
{
    [SerializeField] public Node firstNode;

    public static GameManager instance;

    public PlayerModel model=> _model;

    public bool PlayerExists { get => _playerExists; private set => _playerExists = value; }

    bool _playerExists;

    [SerializeField] PlayerModel _model;

    #region Canvas
    [Header("Canvas")]
    [SerializeField] GameObject VictoryCanvas;
    [SerializeField] GameObject DefeatCanvas;
    [SerializeField] GameObject _waitingPanel;
    [SerializeField] GameObject _selectPanel;
    #endregion


    [SerializeField] GameObject key;

    [Header("Bound")]
    [SerializeField, Range(0, 100)] float width, height, elevation;


    public event Action OnPlayerSet;

    public static Action OnGameModeStart;
    public static bool _hasStarted;

    private void Awake()
    {
        instance = this;
        PlayerExists = false;
        OnGameModeStart += SpawnKey;
    }
    
    public void Defeat()  => RPC_GameOver(false);

    public void Victory() => RPC_GameOver(true);
 
    public void CheckConnectedPlayers()
    {      
        
        if (NetworkRunnerHandler.instance.SessionInfo.PlayerCount > 1)
        {
            RPC_StartGame();

        }
        else
        {
            Debug.Log("A");
            _selectPanel.SetActive(false);
            _waitingPanel.SetActive(true);
        }
    }

    public void SetPlayer(PlayerModel newModel)
    {
        _model = newModel;
        PlayerExists = true;
        OnPlayerSet?.Invoke();
    }

    #region RPC_CALLS
    [Rpc(RpcSources.Proxies, RpcTargets.All)]
    void RPC_StartGame()
    {
        _hasStarted = true;
        Destroy(_selectPanel.gameObject);
        Destroy(_waitingPanel);
        OnGameModeStart?.Invoke();
        Debug.Log("RPC START");
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_GameOver(NetworkBool win)
    {
        if (win) VictoryCanvas.SetActive(true); else DefeatCanvas.SetActive(true);
    }
    #endregion 

    #region KeySpawn
    void SpawnKey()
    {
        if (!HasStateAuthority) return;
        

        
        float xRandom = UnityEngine.Random.Range(-width, width);
        float zRandom = UnityEngine.Random.Range(-height, height);
        Vector3 RandomPos = new Vector3(xRandom, 0, zRandom);
    
        if (Physics.Raycast(ApplyBound(RandomPos),Vector3.down,out RaycastHit hit))
        {
            int layer = hit.transform.gameObject.layer;          
         
            if (layer!=6) SpawnKey();
            
            Debug.Log(hit.transform.gameObject.layer);
            NetworkRunnerHandler.instance.Spawn(key, hit.point, Quaternion.identity);
         

            

            
               
        }
        Debug.Log("no choque con nada");

    }

    public Vector3 ApplyBound(Vector3 objectPosition)
    {
        Vector3 myPos = transform.position;

        if (objectPosition.x > width)
            objectPosition.x = -width;
        if (objectPosition.x < -width)
            objectPosition.x = width;

        if (objectPosition.z > height)
            objectPosition.z = -height;
        if (objectPosition.z < -height)
            objectPosition.z = height;
        objectPosition = new Vector3(objectPosition.x + myPos.x, elevation, objectPosition.z + myPos.z);
        
        return objectPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 myPos = transform.position;

        Vector3 topLeft  =  new Vector3(-width + myPos.x, elevation,  height + myPos.z);
        Vector3 topRight =  new Vector3( width + myPos.x, elevation , height + myPos.z);
        Vector3 botRight =  new Vector3( width + myPos.x, elevation, -height + myPos.z);
        Vector3 botLeft  =  new Vector3(-width + myPos.x, elevation, -height + myPos.z);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, botRight);
        Gizmos.DrawLine(botRight, botLeft);
        Gizmos.DrawLine(botLeft, topLeft);
    }
    #endregion
}
