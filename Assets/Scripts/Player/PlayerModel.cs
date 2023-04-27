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
    [SerializeField] GameObject playerView;
    [SerializeField] Camera _cam;
    [SerializeField] Animator animator;


    public override void Spawned()
    {
        base.Spawned();
        Awakea();
    }

    private void Awakea()
    {
        if (!HasInputAuthority) return;


        animator = playerView.GetComponent<Animator>();
        _ntwkRb = GetComponent<NetworkRigidbody>();
        _movement = new Player_Movement(_speed, _ntwkRb, _jumpForce, transform, playerView, animator);
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
        if (_life<=0) GameManager.instance.Defeat();
        //metodo de morir
    }

 

    public void Move(Vector2 input) => _movement.Move(input.y, input.x);

    public void Jump(bool arg) { if (arg) _movement.Jump(); } 

    public void Shoot(bool arg) => Debug.Log("el personaje no tiene disparo");

    public void Aim(Vector2 input) => Debug.Log("el personaje no tiene apuntado");

    private void OnCollisionEnter(Collision collider)
    {
        _movement.IsGrounded(collider);
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            GameManager.instance.Defeat();
        }
        if (other.gameObject.layer == 8)
        {
            GameManager.instance.Victory();
        }
    }

    public bool InputAuthority() => HasInputAuthority;
}
