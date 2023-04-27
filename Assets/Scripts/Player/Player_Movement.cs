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
    GameObject playerView;
    Animator anim;

    public Player_Movement(float s, NetworkRigidbody rb, float jf, Transform t,GameObject playerView,Animator anim)
    {
        _speed = s;
        _rb = rb;
        _jumpForce = jf;
        _transform = t;
        this.playerView = playerView;
        this.anim = anim;
    }

    public void Jump()
    {
        Debug.Log("Jump");
        isGrounded = false;
        _rb.Rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
    }

    public void Move(float MovV, float MovH)
    {
        Vector3 direction = new Vector3(MovH, 0, MovV);
        if (direction!= Vector3.zero)
        {
            _rb.Rigidbody.MovePosition(_transform.position + (direction.normalized * _speed * Time.deltaTime));
            playerView.transform.forward = direction;         
        }
        bool running = direction != Vector3.zero ? true : false;
        anim.SetBool("isRunning", running);






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
