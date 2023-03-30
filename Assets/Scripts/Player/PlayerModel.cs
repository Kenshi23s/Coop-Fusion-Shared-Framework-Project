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

    private void Awake()
    {
        _rb=this.GetComponent<Rigidbody>();
        _movement = new Player_Movement(_speed, _rb, _jumpForce, transform);
        _inputs = new Player_Inputs(_movement);
    }

    void Start()
    {
        
    }
    private void Update()
    {
        _inputs.ListenInputs();
    }

    private void FixedUpdate()
    {
        _inputs.PlayInputs();
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
