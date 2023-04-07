using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModel 
{
    void Move(Vector2 input);
    void Aim(Vector2 input);
    void Jump();
    void Shoot();
    bool inputAuthority();

}
