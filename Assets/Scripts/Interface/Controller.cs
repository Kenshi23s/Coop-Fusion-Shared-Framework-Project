using System;
using UnityEngine;

public abstract class Controller : MonoBehaviour
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

    private void Update()
    {
        ListenInputs();
        onPlayInput?.Invoke();
        onPlayInput=null;
    }

    public void FixedUpdate()
    {
        onPhysicsPlayInput?.Invoke();
        onPhysicsPlayInput=null;
    }
    protected void AddToPlayPhysics(Action action) => onPhysicsPlayInput += action;

    protected void AddToPlay(Action action) => onPlayInput += action;  

    protected abstract void ListenInputs();
    

}