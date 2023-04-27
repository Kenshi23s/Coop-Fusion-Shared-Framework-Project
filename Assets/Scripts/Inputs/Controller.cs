using System;
using UnityEngine;
using Fusion;

public abstract class Controller : NetworkBehaviour
{
    protected IModel model;

 
    Action _fixedUpdateNetwork;

    //Action onPlayInput;
    //Action onPhysicsPlayInput;

    public void SetModel(IModel model) => this.model = model;
    public override void Spawned()
    {
        base.Spawned();
        Awakea();
    }
    private void Awakea()
    {
        if (!HasInputAuthority) return;
        SetModel(GetComponentInChildren<IModel>());

     

        _fixedUpdateNetwork = () =>
        {       
            if (GetInput(out NetworkInputData networkInputData))
            {
                model.Move(networkInputData.movementInput);
                model.Aim(networkInputData.aimInput);
                model.Jump(networkInputData.isJumpPressed);
                model.Shoot(networkInputData.isFirePressed);
            }
        };
        SpawnNetworkPlayer.SetInputController(this);
    }

    public override void FixedUpdateNetwork() => _fixedUpdateNetwork?.Invoke();

    public abstract bool ListenInputs(out NetworkInputData data);

    #region ModelListen
    protected abstract Vector2 MoveListen();

    protected abstract Vector2 AimListen();
   
    protected abstract bool JumpListen();
   
    protected abstract bool ShootListen();
    #endregion

}