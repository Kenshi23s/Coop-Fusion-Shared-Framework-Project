using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneGuard : MonoBehaviour
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

    DroneInput _myDroneInput;

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
        {
            _cam = Camera.main;
        }
        _myShootingDrone = new DroneShooting(_cam, _bulletStats, OnHit, OnMiss);
        _myMovementDrone = new Drone_Movement(transform,_movementStats,FirstNode, AddToUpdate, SubstractFromUpdate);
        _myDroneInput = new Input_mouse(_myShootingDrone, _myMovementDrone, _cam);
    }

    void Update()
    {
        _myDroneInput.ListenInput();
  
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
    void AddToUpdate(Action method)
    {
        _everyTick+= method;
    }
    void SubstractFromUpdate(Action method)
    {
        _everyTick -= method;
    }
}
