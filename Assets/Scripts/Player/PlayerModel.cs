using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour , IDamagable
{

    Player_Inputs _inputs;
    Player_Movement _movement;

    [SerializeField] float _life;
    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] Rigidbody _rb;   


    void Start()
    {
        _movement = new Player_Movement(_speed, _rb, _jumpForce, transform);
        _inputs = new Player_Inputs(_movement);
    }

    private void FixedUpdate()
    {
        _inputs.Inputs();
    }

    private void OnCollisionEnter(Collision collider)
    {
        _movement.IsGrounded(collider);
    }



    public void TakeDamage(int dmg)
    {
        _life -= dmg;
    }


}
