using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement
{
    public bool isGrounded;
    Transform _transform;
    NetworkRigidbody _rb;
    float _jumpForce;
    float _speed;

    public Player_Movement(float s, NetworkRigidbody rb, float jf, Transform t)
    {
        _speed = s;
        _rb = rb;
        _jumpForce = jf;
        _transform = t;
    }

    public void Jump()
    {
        isGrounded = false;
        _rb.Rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
    }

    public void Move(float MovV, float MovH)
    {
        Vector3 direction = (_transform.forward * MovV) + (_transform.right * MovH);

        _rb.Rigidbody.MovePosition(_transform.position +(direction.normalized * _speed * Time.deltaTime));
    }

    public void IsGrounded(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
            Debug.Log("Estoy tocando piso");
        }
    }


}
