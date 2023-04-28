using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Node firstNode;

    public static GameManager instance;

    [SerializeField] Transform playerSpawnPosf;

    public PlayerModel model=> _model;

    public bool PlayerExists { get => _playerExists; private set => _playerExists = value; }

    bool _playerExists;

    [SerializeField]PlayerModel _model;

    [SerializeField] GameObject VictoryCanvas;
    [SerializeField] GameObject DefeatCanvas;
    [Header("Bound")]
    [SerializeField,Range(0,100)] float width, height, elevation;

    [SerializeField] GameObject key;






    public event Action OnPlayerSet;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        PlayerExists = false;
        SpawnNetworkPlayer.OnGameModeStart += SpawnKey;

    }

    public void Defeat() => DefeatCanvas.SetActive(true);

    public void Victory() => VictoryCanvas.SetActive(true);

    public void SetPlayer(PlayerModel newModel)
    {
        _model = newModel;
        PlayerExists = true;
        OnPlayerSet?.Invoke();
    }

    void SpawnKey()
    {
        float xRandom = UnityEngine.Random.Range(-width, width);
        float zRandom = UnityEngine.Random.Range(-height, height);
        Vector3 RandomPos = new Vector3(xRandom, 0, zRandom);
    
        if (Physics.Raycast(ApplyBound(RandomPos),Vector3.down,out RaycastHit hit))
        {
            int layer = hit.transform.gameObject.layer;
           
         
            if (layer==6)
            {
                Debug.Log(hit.transform.gameObject.layer);
                SpawnNetworkPlayer._currentRunner.Spawn(key, hit.point, Quaternion.identity);
                Debug.Log("Spawnie");
                return;
            }

            SpawnKey();

            
               
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
}
