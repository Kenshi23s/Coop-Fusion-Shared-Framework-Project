using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class NetworkPlayer : NetworkBehaviour
{
   public static NetworkPlayer Local { get; private set; }

    public override void Spawned()
    {
        //Si somos el local
        if (Object.HasInputAuthority)
        {
            Local = this;
            Debug.Log("[Custom Msg] Spawned Own Player");
        }
        else
        {
            Debug.Log("[Custom Msg] Spawned Other Player");
        }

    }
}
