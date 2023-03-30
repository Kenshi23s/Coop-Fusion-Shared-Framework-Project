using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    void SetModel(IModel model);
    void InputShoot();
}