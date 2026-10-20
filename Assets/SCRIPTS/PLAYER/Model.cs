using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player_SM;

public class Model
{
    private Rigidbody _rb;
    private Transform _newTransform;
    private float _maxSpeed;
    private float _currentSpeed;

    private Camera _cam;
    private LayerMask _floorLayer;
    private PlayerStateMachine _pStateMachine;

    public Model(Rigidbody rb, Transform t, PlayerStateMachine s)
    {
        _rb = rb;
        _newTransform = t;
        _pStateMachine = s;
    }

    public Model SetSpeeds(float ms)
    {
        _maxSpeed = ms;
        return this;
    }

    public Model SetCamera(Camera c, LayerMask l)
    {
        _cam = c;
        _floorLayer = l;
        return this;
    }

    public PlayerStateMachine GetStateMachine() => _pStateMachine;

    public void MoveToPoint(Transform t)
    {
        Vector3 direction = (t.position - _rb.position).normalized;

        var newPos = _rb.position + direction * _maxSpeed * Time.deltaTime;

        Vector3 dir = Vector3.MoveTowards(
            _rb.position, 
            t.position,
            _maxSpeed * Time.deltaTime
        );

        _rb.MovePosition(dir);
    }

    public void Movement(float x, float y)
    {
        if(x == 0 && y == 0)
        {
            _pStateMachine.TryChange(PlayerState.Idle);
            return;
        }

        Vector3 direction = _newTransform.forward * x ;
        _pStateMachine.TryChange(PlayerState.Walking);
        _rb.MovePosition(_newTransform.position + direction * _maxSpeed * Time.deltaTime);
    }

    public void Rotation(float x, float y)
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100f, _floorLayer))
        {
            Vector3 pointMouse = hit.point;

            Vector3 direccion = pointMouse - _newTransform.position;
            direccion.y = 0f; // evita mirar arriba/abajo

            if (direccion.sqrMagnitude > 0.01f)
            {
                Quaternion rotacion = Quaternion.LookRotation(direccion);
                _newTransform.rotation = rotacion;
            }        
        }
    }
}
