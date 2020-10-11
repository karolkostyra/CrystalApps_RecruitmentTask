using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] private float distToGround = 0.2f;
    [SerializeField] private LayerMask ground;

    float distToGround;

    private float walkSpeed = 100.0f;
    private float runSpeed = 150.0f;
    private float jumpForce = 5.0f;

    private Rigidbody _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        runSpeed = walkSpeed + walkSpeed / 2;
    }

    void Update()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;

        if (Input.GetKey(KeyCode.W))
        {
            _rb.AddForce(walkSpeed * transform.forward);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _rb.AddForce(runSpeed * transform.forward);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            _rb.AddForce(walkSpeed * -transform.right);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rb.AddForce(walkSpeed * transform.right);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _rb.AddForce((walkSpeed - walkSpeed / 4) * -transform.forward);
        }
        if (Input.GetKey(KeyCode.Space) && Grounded())
        {
            //Vector3 up = transform.TransformDirection(Vector3.up);
            //_rb.AddForce( up * 0.5f, ForceMode.Impulse);
            _rb.velocity = Vector3.up * 5f;            
        
        //_rb.AddForce((Vector3.up * 1), ForceMode.Impulse);
        }

        Debug.Log(Grounded());
    }

    private bool Grounded()
    {
        Debug.Log("DIST" + distToGround);
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        return Physics.Raycast(pos, Vector3.down, 0.105f, ground);
    }

    private void Jump()
    {
        float yVel = _rb.velocity.y;
        Vector3 newVel = transform.forward * 10 + transform.right;
        newVel = newVel.normalized * 10;
        newVel.y = yVel;
        _rb.velocity = newVel;
    }
}
