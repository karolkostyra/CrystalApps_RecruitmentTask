using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 100.0f;
    [SerializeField] private float runSpeed = 150.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float distToGround = 0.105f;
    [SerializeField] private LayerMask ground;

    [SerializeField] private KeyCode forwardKey = KeyCode.W;
    [SerializeField] private KeyCode backwardKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode runKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    private Animator _animator;
    private Rigidbody _rb;

    private bool forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed, jumpPressed;
    private float walkVelocity = 0.5f;
    private float runVelocity = 2.0f;
    private float acceleration = 2.0f;
    private float deceleration = 2.0f;
    private float velocityX, velocityZ = 0.0f;
    private float currentVelocity;
    private float _walkSpeed, _runSpeed;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        SetSpeed();
    }
    
    void Update()
    {
        HandleInput();

        currentVelocity = runPressed ? runVelocity : walkVelocity;

        if (!Grounded())
        {
            _runSpeed = _walkSpeed;
            //runSpeed = walkSpeed = walkSpeed / 2;
        }
        else
        {
            SetSpeed();
        }

        Move();
        HandleAcceleration();
        HandleDeceleration();

        _animator.SetFloat("VelocityX", velocityX);
        _animator.SetFloat("VelocityZ", velocityZ);
    }

    private void SetSpeed()
    {
        _runSpeed = runSpeed;
        _walkSpeed = walkSpeed;
    }

    private void HandleAcceleration()
    {
        //move forward animation
        if ((forwardPressed || runPressed) && velocityZ < currentVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
            //float currentSpeed = runPressed ? _runSpeed : walkSpeed;
        }
        //move back animation
        if (backwardPressed && velocityZ > -currentVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        //move to left animation
        if (leftPressed && velocityX > -currentVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        //move to right animation
        if (rightPressed && velocityX < currentVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }
    }

    private void HandleDeceleration()
    {
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
        if(!runPressed && velocityZ > 0.5f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
        if(!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
        if(!backwardPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }
    }

    private void HandleInput()
    {
        forwardPressed = Input.GetKey(forwardKey);
        backwardPressed = Input.GetKey(backwardKey);
        leftPressed = Input.GetKey(leftKey);
        rightPressed = Input.GetKey(rightKey);
        runPressed = Input.GetKey(runKey);
        jumpPressed = Input.GetKey(jumpKey);
    }

    private void Move()
    {
        if (forwardPressed)
        {
            _rb.AddForce(_walkSpeed * transform.forward);
            if (runPressed)
            {
                _rb.AddForce(_runSpeed * transform.forward);
            }
        }
        if (leftPressed)
        {
            _rb.AddForce(_walkSpeed * -transform.right);
        }
        if (rightPressed)
        {
            _rb.AddForce(_walkSpeed * transform.right);
        }
        if (backwardPressed)
        {
            _rb.AddForce((_walkSpeed - _walkSpeed / 4) * -transform.forward);
        }
        if (jumpPressed && Grounded())
        {
            _rb.velocity = Vector3.up * jumpForce;
        }
    }

    private bool Grounded()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        return Physics.Raycast(pos, Vector3.down, 0.105f, ground);
    }
}
