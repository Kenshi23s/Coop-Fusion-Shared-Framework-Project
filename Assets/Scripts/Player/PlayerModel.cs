using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour , IDamagable,IModel
{

    //Player_Inputs _inputs;
    Player_Movement _movement;

    [SerializeField] float _life;
    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] Rigidbody _rb;

    private void Awake()
    {
        _rb=this.GetComponent<Rigidbody>();
        _movement = new Player_Movement(_speed, _rb, _jumpForce, transform);
        //_inputs = new Player_Inputs(_movement);
    }

    private void OnCollisionEnter(Collision collider)
    {
        _movement.IsGrounded(collider);
    }


    public void TakeDamage(int dmg)
    {
        _life -= dmg;
    }

    public void Move(Vector2 input) => _movement.Move(input.y, input.x);

    public void Jump() => _movement.Jump();

    public void Shoot()
    {
        Debug.Log("el personaje no tiene disparo");
    }

    public void Aim(Vector2 input)
    {
        Debug.Log("el personaje no tiene apuntado");
    }
}
