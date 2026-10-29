using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Player_SM;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float speed;
    public Camera camera;
    public LayerMask floorMask;
    private Rigidbody _rb;
    private Controller _controller;
    private View _view;
    private Model _model;

    private PlayerStateMachine _stateMachine;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();

        _stateMachine = new PlayerStateMachine();

        _model = new Model(_rb, transform, _stateMachine).SetSpeeds(speed).SetCamera(camera, floorMask);
        _view = new View();
        _controller = new Controller(_model);

    }

    void Update()
    {
        _controller.ArtificialUpdate();
    }

    void FixedUpdate()
    {
        _controller.ArtificialFixedUpdate();
    }
}
