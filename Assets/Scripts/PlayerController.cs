using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float MoveForce = 50.0f;
    [SerializeField] float JumpForce = 10.0f;
    [SerializeField] float MaxMoveSpeed = 10.0f;
    [SerializeField] float MaxFallSpeed = 10.0f;

    private Rigidbody rigidBody;

    private float ClampToSpeed(float val, float speed) { return Mathf.Clamp(val, speed * -1, speed); }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        rigidBody.AddForce(movement * MoveForce);

        rigidBody.velocity = new Vector3(
            ClampToSpeed(rigidBody.velocity.x, MaxMoveSpeed),
            0,
            ClampToSpeed(rigidBody.velocity.z, MaxMoveSpeed)
        );

        rigidBody.velocity.Set(rigidBody.velocity.x, ClampToSpeed(rigidBody.velocity.y, MaxFallSpeed), rigidBody.velocity.z);


        if (Input.GetButtonDown("PlayerJump"))
        {
            rigidBody.AddForce(new Vector3(0, JumpForce, 0));
        }
    }
}
