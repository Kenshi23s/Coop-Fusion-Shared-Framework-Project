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

    public void Defeat()
    {
        DefeatCanvas.SetActive(true);
        
    }
    public void Victory()
    {
        VictoryCanvas.SetActive(true);
    }

    public event Action OnPlayerSet;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        PlayerExists = false;

    }  

    public void SetPlayer(PlayerModel newModel)
    {
        _model = newModel;
        PlayerExists = true;
        OnPlayerSet?.Invoke();
    } 
 
}
