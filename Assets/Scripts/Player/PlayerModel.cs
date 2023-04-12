using Fusion;
using UnityEngine;

[RequireComponent(typeof(NetworkRigidbody))]
public class PlayerModel : NetworkBehaviour , IDamagable, IModel
{

    Player_Movement _movement;
    
    [SerializeField] float _life;
    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] NetworkRigidbody _ntwkRb;

    [SerializeField] Camera _cam;

    private void Awake()
    {
        _ntwkRb = GetComponent<NetworkRigidbody>();
        _movement = new Player_Movement(_speed, _ntwkRb, _jumpForce, transform);
        GameManager.instance.SetPlayer(this);
        if (_cam!=null)
        {
            Destroy(Camera.main);
            Camera.SetupCurrent(_cam);
        }
        //_inputs = new Player_Inputs(_movement);
    }  
    

    public void TakeDamage(int dmg)
    {
        _life -= dmg;
        //metodo de morir
    }

    public void Move(Vector2 input) => _movement.Move(input.y, input.x);

    public void Jump(bool arg) => _movement.Jump();

    public void Shoot(bool arg) => Debug.Log("el personaje no tiene disparo");

    public void Aim(Vector2 input) => Debug.Log("el personaje no tiene apuntado");

    private void OnCollisionEnter(Collision collider) => _movement.IsGrounded(collider);

    public bool InputAuthority() => HasInputAuthority;
}
