using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FacundoColomboMethods;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField]public List<Transform> DroneWaypoints;
    public static GameManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance= this;
        DroneWaypoints = ColomboMethods.GetChildrenComponents<Transform>(this.transform).ToList();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
