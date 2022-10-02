using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] CharacterController characterController;
    [SerializeField] float MoveSpeed = 10.0f;
    [SerializeField] float MaxFallSpeed = 10.0f;
    [SerializeField] float JumpHeight = 3.5f;

    [Header("Camera")]
    [SerializeField] float SensitivityX = 8f;
    [SerializeField] float SensitivityY = 0.5f;
    [SerializeField] bool LockCursorOnStart = true;
    [SerializeField] Transform PlayerCamera;
    [SerializeField] float LookXClamp = 85f;

    [Header("Environment")]
    [SerializeField] float GravityAcceleration = 30.0f;
    [SerializeField] LayerMask GroundLayerMask;

    private Vector2 horizontalInput;

    private float verticalVelocity = 0f;

    private bool isGrounded = false;
    private bool isJumping = false;

    private float mouseX, mouseY;
    private float xRotation = 0f;

    private float ClampAbsolute(float val, float factor) { return Mathf.Clamp(val, factor * -1, factor); }

    public void OnMove(InputValue input)
    {
        horizontalInput = input.Get<Vector2>();
    }

    public void OnLook(InputValue input)
    {
        mouseX = input.Get<Vector2>().x;
        mouseY = input.Get<Vector2>().y;
    }

    public void OnJump()
    {
        isJumping = true;
    }
    public void OnDDone()
    {
        Debug.Log("rfrsdhfhj");
    }

    private void Awake()
    {
        Cursor.lockState = (LockCursorOnStart) ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //CAMERA LOOK
        //Turning left and right
        transform.Rotate(Vector3.up, mouseX * SensitivityX * Time.smoothDeltaTime);
        //looking up and down
        xRotation -= mouseY * SensitivityY * Time.smoothDeltaTime;
        xRotation = ClampAbsolute(xRotation, LookXClamp);
        Vector3 targetRotation = PlayerCamera.eulerAngles;
        targetRotation.x = xRotation;
        PlayerCamera.eulerAngles = targetRotation;

        //MOVEMENTS
        isGrounded = Physics.CheckSphere(transform.position, characterController.height, GroundLayerMask);
        if (isGrounded)
        {
            if (isJumping)
            {
                verticalVelocity = Mathf.Sqrt(2f * JumpHeight * GravityAcceleration);
                isJumping = false;
            } else
            {
                verticalVelocity = 0;
            }
        } else
        {
            verticalVelocity += GravityAcceleration * Time.deltaTime * -1;
        }

        //falling/jumping
        characterController.Move(transform.up * Time.deltaTime * ClampAbsolute(verticalVelocity, MaxFallSpeed));
        //horizontal movement
        characterController.Move((transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * MoveSpeed * Time.deltaTime);
    }
}
