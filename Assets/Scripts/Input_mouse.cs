using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_mouse : DroneInput
{
    KeyCode DesiredShootInput;
    Dictionary<KeyCode, NodeDirection> _keys = new Dictionary<KeyCode, NodeDirection>();
    public Input_mouse(DroneShooting droneShoot, Drone_Movement dronemovement, Camera cam) : base(droneShoot, dronemovement,  cam)
    {
        DroneGuard.DroneGizmos += InputGizmos;

        _keys.Add(KeyCode.W, NodeDirection.Up);
        _keys.Add(KeyCode.A, NodeDirection.Left);
        _keys.Add(KeyCode.S, NodeDirection.Down);
        _keys.Add(KeyCode.D, NodeDirection.Right);
    }

    

    protected override bool MovementInput(out NodeDirection direction)
    {
        foreach (KeyCode actualKey in _keys.Keys)
        {
            if (Input.GetKeyDown(actualKey))
            {
                Debug.Log($"se presiono la tecla {actualKey}");
                direction = _keys[actualKey];
                return true;
            }
        }
        direction = default;
        return false;
    }

    protected override bool ShootInput(out Ray ray)
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            return true;
        }
        ray = default;
        return false;
    }

    public override void InputGizmos()
    {
        Gizmos.color = Color.red;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(new Vector3(hit.point.x, hit.point.y, hit.point.z), 6f);

        }
    }
}
