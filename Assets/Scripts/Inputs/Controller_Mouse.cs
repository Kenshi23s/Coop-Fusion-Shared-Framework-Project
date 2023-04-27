using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Mouse : Controller
{

    [SerializeField] KeyCode shoot;
    [SerializeField] KeyCode jump;



    public override bool ListenInputs(out NetworkInputData data)
    {
        //podria tener un metodo de awake y sumarle estos metodos a un action ahi,
        //pero para que quede mas legible decidi dejarlo asi 
        if (!model.InputAuthority())
        {
            data = default;
            Debug.Log("NO TENGO AUTORIDAD DE INPUT");
            return false;

        }
            

        data = new NetworkInputData();
        data.movementInput = MoveListen();
        data.aimInput = AimListen();
        data.isJumpPressed = JumpListen();
        data.isFirePressed = ShootListen();
        return true;
    } 
   
    protected override Vector2 MoveListen()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y);

        //if (x != 0 || y != 0)
        //{
        //    AddToPlayPhysics(() => model.Move(new Vector2(x, y)));
        //    //Debug.Log($"x es {x}, e y es {y}");
        //}       
    }

    protected override bool ShootListen()
    {
        return Input.GetKeyDown(shoot);
        //AddToPlay(() => model.Shoot());        
    }

    protected override bool JumpListen()
    {

        return Input.GetKeyDown(jump);
       //AddToPlayPhysics(() => model.Jump());
    }

    protected override Vector2 AimListen()
    {
        return Input.mousePosition;
        //AddToPlayPhysics(() => model.Aim());
    }
}

