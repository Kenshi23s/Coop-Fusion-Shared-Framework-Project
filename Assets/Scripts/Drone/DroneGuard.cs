using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class DroneGuard : NetworkBehaviour , IModel
{
    #region DroneComponents

    #region Shooting Component
         DroneShooting _myShootingDrone;
         [SerializeField] BulletStats _bulletStats;

    #endregion

    #region Movement Components
         Drone_Movement _myMovementDrone;
         [SerializeField] DroneMovementStats _movementStats;
         [SerializeField] Node FirstNode;

    #endregion

    #region Input

    Drone_CrossHair _myCrosshairDrone;

    #endregion

    #endregion


    [SerializeField]Camera _cam;
   

    

    [SerializeField] bool drawGizmos = true;

    public static event Action DroneGizmos;
    public static event Action OnHit;
    public static event Action OnMiss;

    internal Action _everyTick;

    private void Awake()
    {
        if (_cam==null)
           _cam = Camera.main;
        
        Action<Action> _add = (x) => _everyTick += x;
        Action<Action> _substract = (x) => _everyTick -= x;

        _myCrosshairDrone = new Drone_CrossHair();
        //primero genero el crosshair pq despues se lo paso al shooting
        _myShootingDrone = new DroneShooting(_cam, _bulletStats, OnHit, OnMiss, _myCrosshairDrone);
        _myMovementDrone = new Drone_Movement(transform,_movementStats,FirstNode, _add, _substract);
      

        //_myDroneInput = new Input_mouse(_myShootingDrone, _myMovementDrone, _cam);
    }

    void FixedUpdate()
    {
        //_myDroneInput.ListenInputs();
  
        _everyTick?.Invoke();
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            DroneGizmos?.Invoke();
            Gizmos.DrawWireSphere(transform.position, 3f);
        }
       
    }

    public void Move(Vector2 input)
    {
        _myMovementDrone.SetDirection(Node.GetDirectionNode(input));
    }

    public void Jump()
    {
        Debug.Log( "El dron no tiene Salto" );
    }

    public void Shoot()
    {
        _myShootingDrone.Shoot();
    }

    public void Aim(Vector2 input)
    {
        _myCrosshairDrone.AddCrossHairPos(input);
    }

    public bool inputAuthority()
    {
        return HasInputAuthority;
    }
}
