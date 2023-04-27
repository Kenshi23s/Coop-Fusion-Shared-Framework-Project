using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class DroneGuard : NetworkBehaviour, IModel
{
    #region DroneComponents

    #region Shooting Component
    DroneShooting _myShootingDrone;
    [SerializeField] BulletStats _bulletStats;

    #endregion

    #region Movement Components
    Drone_Movement _myMovementDrone;
    [SerializeField] DroneMovementStats _movementStats;
    [SerializeField] Node firstNode;

    #endregion

    #region Input

    Drone_CrossHair _myCrosshairDrone;

    #endregion

    #endregion


    [SerializeField] Camera _cam;




    [SerializeField] bool drawGizmos = true;

    public static event Action DroneGizmos;
    public static event Action OnHit;
    public static event Action OnMiss;

    Action _everyTick;

    public override void Spawned()
    {
        base.Spawned();
        Awakea();
    }
    private void Awakea()
    {
        if (!HasInputAuthority) return;

        if (_cam != null)
        {
            Destroy(Camera.main);
            Camera.SetupCurrent(_cam);
            firstNode = GameManager.instance.firstNode;
            transform.position = firstNode.transform.position;


        }


        Action<Action> _add = (x) => _everyTick += x;
        Action<Action> _substract = (x) => _everyTick -= x;

        _myCrosshairDrone = new Drone_CrossHair();
        //primero genero el crosshair pq despues se lo paso al shooting
        _myShootingDrone = new DroneShooting(_cam, _bulletStats, OnHit, OnMiss, _myCrosshairDrone);
        _myMovementDrone = new Drone_Movement(transform, _movementStats, firstNode, _add, _substract);


        //_myDroneInput = new Input_mouse(_myShootingDrone, _myMovementDrone, _cam);
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();
        _everyTick?.Invoke();
    }

    private void OnDrawGizmos()
    {

        if (drawGizmos&& _myCrosshairDrone!=null)
        {
            if (!HasInputAuthority) return;

            DroneGizmos?.Invoke();
            if (Physics.Raycast(_myCrosshairDrone.GetCrossHairScreenRay(), out RaycastHit hit, Mathf.Infinity))
            {
                Gizmos.DrawWireSphere(hit.point, _bulletStats.bulletRadius);
            }
          
          
          
            Gizmos.DrawWireSphere(transform.position, 3f);
        }

    }

    public void Move(Vector2 input) => _myMovementDrone.SetDirection(Node.GetDirectionNode(input));

    public void Jump(bool arg) {  }

    public void Shoot(bool arg) { if (arg) _myShootingDrone.Shoot(); }

    public void Aim(Vector2 input) { _myCrosshairDrone.AddCrossHairPos(input); }

    public bool InputAuthority() => HasInputAuthority;

  
}
