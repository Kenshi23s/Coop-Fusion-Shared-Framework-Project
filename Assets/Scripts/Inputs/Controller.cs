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
    private void Awake()
    {
        SetModel(GetComponentInChildren<IModel>());
      

        _fixedUpdateNetwork = () =>
        {
          
            if (GetInput(out NetworkInputData networkInputData))
            {

                model.Move(networkInputData.movementInput);
                model.Aim(networkInputData.aimInput);
                model.Jump(networkInputData.isJumpPressed);
                model.Shoot(networkInputData.isFirePressed);
                //model play
                //onPlayInput?.Invoke();
                //onPlayInput = null;               
                //onPhysicsPlayInput?.Invoke();
                //onPhysicsPlayInput = null;
            }
        };
        SpawnNetworkPlayer.SetInputController(this);
    }

    private void Update()
    {
        //if (!model.InputAuthority()) return;
        
    }

    public override void FixedUpdateNetwork() => _fixedUpdateNetwork?.Invoke();
 
  
    //protected void AddToPlayPhysics(Action action) => onPhysicsPlayInput += action;

    //protected void AddToPlay(Action action) => onPlayInput += action;

    public abstract bool ListenInputs(out NetworkInputData data);

    #region ModelListen
    protected abstract Vector2 MoveListen();

    protected abstract Vector2 AimListen();
   
    protected abstract bool JumpListen();
   
    protected abstract bool ShootListen();
    #endregion

}