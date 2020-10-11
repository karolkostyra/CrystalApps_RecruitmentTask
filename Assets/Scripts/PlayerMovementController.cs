using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovementController : MonoBehaviour
{
    private float walkSpeed = 100.0f;
    private float runSpeed = 150.0f;

    private Rigidbody _rb;
    private Animator _animator;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        runSpeed = walkSpeed + walkSpeed / 2;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rb.AddForce(walkSpeed * transform.forward);
            _animator.SetFloat("speed", 0.5f);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _rb.AddForce(runSpeed * transform.forward);
                _animator.SetFloat("speed", 1.0f);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            _rb.AddForce(walkSpeed * -transform.right);
            _animator.SetFloat("speed", 0.25f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rb.AddForce(walkSpeed * transform.right);
            _animator.SetFloat("speed", 0.75f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _rb.AddForce((walkSpeed - walkSpeed / 4) * -transform.forward);
            _animator.SetFloat("speed", -1.0f);
        }
        else
        {
            _animator.SetFloat("speed", 0.0f);
        }
    }
}
