using System;
using UnityEngine;
using Fusion;

public abstract class Controller : NetworkBehaviour
{
    protected IModel model;

    protected float x;
    protected float y;



    Action onPlayInput;
    Action onPhysicsPlayInput;

    public void SetModel(IModel model) => this.model = model;
    private void Awake()
    {
        SetModel(GetComponentInChildren<IModel>());
    }

    public override void FixedUpdateNetwork()
    {

        if (GetInput(out NetworkInputData networkInputData))
        {
            ListenInputs();
            onPlayInput?.Invoke();
            onPlayInput = null;
            onPhysicsPlayInput?.Invoke();
            onPhysicsPlayInput = null;
        }

    }
    protected void AddToPlayPhysics(Action action) => onPhysicsPlayInput += action;

    protected void AddToPlay(Action action) => onPlayInput += action;

    protected abstract void ListenInputs();


}