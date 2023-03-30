using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement
{
    public bool isGrounded;
    Transform _transform;
    Rigidbody _rb;
    float _jumpForce;
    float _speed;

    public Player_Movement(float s, Rigidbody rb, float jf, Transform t)
    {
        _speed = s;
        _rb = rb;
        _jumpForce = jf;
        _transform = t;
    }

    public void Jump()
    {
        isGrounded = false;
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
    }

    public void Move(float MovV, float MovH)
    {
        var direction = _transform.forward * MovV;
        direction += _transform.right * MovH;

        _transform.position += direction * _speed * Time.deltaTime;
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
