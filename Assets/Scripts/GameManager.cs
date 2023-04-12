using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Node firstNode;
    public static GameManager instance;
    [SerializeField] Transform playerSpawnPosf;
    public PlayerModel model=> _model;
    [SerializeField]PlayerModel _model;
    // Start is called before the first frame update
    private void Awake() => instance = this;     

    public void SetPlayer(PlayerModel newModel) => _model = newModel;
 
}
