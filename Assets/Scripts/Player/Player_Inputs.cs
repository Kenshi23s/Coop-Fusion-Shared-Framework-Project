using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inputs
{
    Player_Movement _movement;

    public Player_Inputs(Player_Movement pm)
    {
        _movement = pm;
    }

    public void Inputs()
    {
        var MovV = Input.GetAxis("PlayerVertical");
        var MovH = Input.GetAxis("PlayerHorizontal");

        if (MovV != 0 || MovH != 0)
        {
            _movement.Move(MovV, MovH);            
        }        

        if (_movement.ig)
        {
            if (Input.GetKey(KeyCode.Space))
            {               
                _movement.Jump();
            }    
        }
    }
}
