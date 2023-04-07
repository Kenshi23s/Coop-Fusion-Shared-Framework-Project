using Fusion;
using UnityEngine;

public class PlayerModel : NetworkBehaviour , IDamagable,IModel
{

    //Player_Inputs _inputs;
    Player_Movement _movement;

    [SerializeField] float _life;
    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] NetworkRigidbody _rb;
    //[SerializeField] Camera camera;

    private void Awake()
    {
        
        _movement = new Player_Movement(_speed, _rb.Rigidbody, _jumpForce, transform);
        //_inputs = new Player_Inputs(_movement);
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

    private void OnCollisionEnter(Collision collider)
    {
        _movement.IsGrounded(collider);
    }
}
