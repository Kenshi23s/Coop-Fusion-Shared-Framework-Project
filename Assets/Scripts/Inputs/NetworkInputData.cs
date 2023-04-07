using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct NetworkInputData : INetworkInput
{
    public Vector2 movementInput;
    public Vector2 aimInput;
    

    public NetworkBool isJumpPressed;
    public NetworkBool isFirePressed;

    //public NetworkInputData(Vector2 movementInput, Vector2 aimInput, NetworkBool isJumpPressed, NetworkBool isFirePressed)
    //{
    //    this.movementInput = movementInput;
    //    this.aimInput = aimInput;
    //    this.isJumpPressed = isJumpPressed;
    //    this.isFirePressed = isFirePressed;
    //}
}
