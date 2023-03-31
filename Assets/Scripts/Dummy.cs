using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour, IDamagable
{
    [SerializeField]int life = 100;
    public void TakeDamage(int dmg)
    {
        life -= dmg;
        Debug.Log(life);
        if (life<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
