using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterInputHandler : NetworkBehaviour
{
    PlayerModel _model;
    float _moveHInput;
    float _moveVInput;
    bool _isJumpPressed;
    bool _isFirePressed;

    private void Start()
    {
        _model = GetComponent<PlayerModel>();
    }

    private void Update()
    {
        //si no tenes autpridad de input, entonces no haces lo siguiente.
        if (!_model.HasInputAuthority) return;

        _moveHInput = Input.GetAxis("Horizontal");
        _moveVInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
            {     
              _isJumpPressed= true;
            }    

        //if input entonces _isFirePressed true;

    }

    //Metodo ejecutado por el Spawner, le provee todos los inputs del frame actual
    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData inputData = new NetworkInputData();

        inputData.movementHInput = _moveHInput;
        inputData.movementVInput = _moveVInput;

        inputData.isJumpPressed = _isJumpPressed;
        _isJumpPressed = false;

        //lo mismo con el isfirepressed

        return inputData;

    }
}
