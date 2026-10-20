using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player_SM;

public class Controller 
{
    private Model _model;
    private PlayerStateMachine _pStateMachine;

    public Controller(Model m)
    {
        _model = m;
        _pStateMachine = _model.GetStateMachine();

        if (_pStateMachine != null) Debug.Log("HAY MACHINE EN CONTROLLER");
        else Debug.Log("no hay machine en controller");
    }

    public void ArtificialUpdate()
    {
        if(GameManager.instance.isCoffeOpen || GameManager.instance.isInventoryOpen)
        {
            _pStateMachine.TryChange(PlayerState.NoMove);
            return;
        }
        else
        {
            _pStateMachine.TryChange(PlayerState.Idle);
        }
    }

    public void ArtificialFixedUpdate()
    {
        if (_pStateMachine.Current == PlayerState.NoMove) return;

        _model.Movement(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        _model.Rotation(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
    }
}
