using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeDirection
{
    Left,Right,Up,Down,None
}
[System.Serializable]
public struct NeihgborNode
{
    public NodeDirection Direction;
    public Node node;
}
public class Node : MonoBehaviour
{
    public static NodeDirection GetDirectionNode(Vector2 move)
    {
         
        if (move.x > 0) return NodeDirection.Right;
        if (move.x < 0) return NodeDirection.Left;

        if (move.y > 0) return NodeDirection.Up;
        if (move.y < 0) return NodeDirection.Down;


        return NodeDirection.None;
    }

    [SerializeField]List< NeihgborNode> neihgborNodes=new List<NeihgborNode>();
   
    public Dictionary<NodeDirection,Node> NodeList= new Dictionary<NodeDirection,Node>();
   

    private void Awake()
    {

        foreach (var item in neihgborNodes)
        {
            AddNode(item.node, item.Direction);
        }
    }

    void AddNode(Node node, NodeDirection direction)
    {
        if (node!=null && !NodeList.ContainsKey(direction))
        {
            NodeList.Add(direction,node);
            return;
        }
    }

    public bool HasNeighbor(NodeDirection direction)
    {
        if (NodeList.ContainsKey(direction))
        {
            Debug.Log("tengo un node en esa direccion!");
            return true;
        }
        return false;
       
    }  

    public Node GetNode(NodeDirection direction) => NodeList[direction];

    private void OnDrawGizmos()
    {
        foreach (Node node in NodeList.Values)
        {
            foreach (Node neighbor in node.NodeList.Values)
            {
                if (neighbor == this)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(node.transform.position, transform.position);
                    Gizmos.DrawWireSphere(transform.position, 3f);
                }

                else
                {
                    Gizmos.color = Color.cyan;
                }

            }
        }




        //Gizmos.DrawWireSphere(transform.position, AgentsManager.instance.nodeInteractradius);
    }
}
