using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Mouse : Controller
{

    [SerializeField] KeyCode shoot;
    [SerializeField] KeyCode jump;

  

    protected override void ListenInputs()
    {
        //podria tener un metodo de awake y sumarle estos metodos a un action ahi,
        //pero para que quede mas legible decidi dejarlo asi 
        MoveListen();
        AimListen();
        ShootListen();
        JumpListen();
    }
  
   

    void MoveListen()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (x != 0 || y != 0)
        {
            AddToPlayPhysics(() => model.Move(new Vector2(x, y)));
            //Debug.Log($"x es {x}, e y es {y}");
        }
       
    }

    void ShootListen()
    {
        if (Input.GetKeyDown(shoot))
        AddToPlay(() => model.Shoot());        
    }

    void JumpListen()
    {
       if (Input.GetKeyDown(jump))
       AddToPlayPhysics(() => model.Jump());
    }

    void AimListen()
    {
        AddToPlayPhysics(() => model.Aim(Input.mousePosition));
    }
}

