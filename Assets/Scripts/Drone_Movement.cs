using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct DroneMovementStats
{
   public float speed;
   public float waypointRadius;
   public float movementCD;
}
public class Drone_Movement 
{
    Transform myTransform;
    DroneMovementStats stats;
    float actualCD;

    Action<Action> AddMethod;
    Action<Action> RemoveMethod;

    public Node actualnode=> _actualNode;
    Node _actualNode;
    Node _nextNode;

   public Drone_Movement(Transform myTransform, DroneMovementStats stats, Node _actualNode,Action<Action> AddMethod, Action<Action> RemoveMethod)
   {
       this.myTransform = myTransform;
       this.stats = stats;
       this._actualNode = _actualNode;
       this.AddMethod = AddMethod;
       this.RemoveMethod = RemoveMethod;



        actualCD = 0;
        myTransform.position=actualnode.transform.position;
   }

    public bool CanMove() => actualCD <= 0 ? true : false;
    //public bool CanMove()
    //{
    //    if (actualCD<=0)
    //    {
    //        Debug.Log("no estoy en cd, puedo moverme");
    //        return true;
    //    }
    //    return false;
    //}

   public bool CheckDirection(NodeDirection direction) => _actualNode.HasNeighbor(direction);
   
   public void SetDirection(NodeDirection direction)
   {
        if (CanMove() && CheckDirection(direction))
        {
           
            _nextNode = _actualNode.GetNode(direction);
            AddMethod(MoveDrone);
            RemoveMethod(CoolDownDecrease);
        }      
   }
    
    void MoveDrone()
    {
        if (_nextNode!=null)
        {
            Vector3 dir = _nextNode.transform.position - myTransform.position;
            float t = stats.speed * Time.deltaTime;

            myTransform.position = Vector3.Lerp(myTransform.position, _nextNode.transform.position, t);

            Debug.Log($"me estoy moviendo hacia el nodo {_nextNode.gameObject.name}");

            if (dir.magnitude <= stats.waypointRadius)
            {
                myTransform.position = _nextNode.transform.position;

                _actualNode = _nextNode;
                _nextNode = null;

                actualCD = stats.movementCD;

                AddMethod(CoolDownDecrease);
                RemoveMethod(MoveDrone);
            }
        }
        
    }

    void CoolDownDecrease()
    {
        Debug.Log("cd decrease");
        Debug.Log(actualCD);

        //actualCD = Mathf.Clamp(actualCD - Time.deltaTime, 0, actualCD );
        actualCD = Mathf.Max(actualCD - Time.deltaTime, 0);
        if (actualCD <= 0)
        {
            RemoveMethod(CoolDownDecrease);
        }
    }
}
