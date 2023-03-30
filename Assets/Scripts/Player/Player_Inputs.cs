using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inputs
{
    Player_Movement _movement;
    float MovV;
    float MovH;

    bool jump;
    public Player_Inputs(Player_Movement pm)
    {
        _movement = pm;
    }

    public void ListenInputs()
    {
        var MovV = Input.GetAxis("PlayerVertical");
        var MovH = Input.GetAxis("PlayerHorizontal");

        if (MovV != 0 || MovH != 0)
        {
            _movement.Move(MovV, MovH);            
        }        

        if (_movement.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {     
                jump= true;
                
            }    
        }
    }

    public void PlayInputs()
    {
        if (MovV != 0 || MovH != 0)
        {
            _movement.Move(MovV, MovH);
        }
        if (jump)
        {
            _movement.Jump();
            jump= false;
        }
    }
}
