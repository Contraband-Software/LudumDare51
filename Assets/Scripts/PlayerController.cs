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
    [SerializeField, Min(0)] float MaxFallSpeed = 10.0f;
    [SerializeField, Min(0)] float JumpHeight = 3.5f;
    [SerializeField, Range(0, 1)] float CrouchHeightPercentage = 0.3f;

    [Header("Camera")]
    [SerializeField] float SensitivityX = 8f;
    [SerializeField] float SensitivityY = 0.5f;
    [SerializeField] bool LockCursorOnStart = true;
    [SerializeField] Transform PlayerCamera;
    [SerializeField, Min(0)] float LookXClamp = 85f;

    [Header("Environment")]
    [SerializeField] float GravityAcceleration = 30.0f;
    [SerializeField] LayerMask GroundLayerMask;


    private UI_Controller uiCon;

    private Vector2 horizontalInput;

    private float verticalVelocity = 0f;

    private bool isGrounded = false;
    private bool isJumping = false;

    private float mouseX, mouseY;
    private float xRotation = 0f;

    private float OriginalHeight;

    IEnumerator fadeCrouchDown;
    IEnumerator fadeCrouchUp;

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

    public void OnJump(InputValue input)
    {
        Debug.Log(input.isPressed);
        isJumping = true;
    }

    IEnumerator FadeCrouchDown()
    {
        Debug.Log("START CROUCH");
        while (characterController.height - OriginalHeight * CrouchHeightPercentage > 0.1)
        {
            characterController.height -= (characterController.height - OriginalHeight * CrouchHeightPercentage) * 0.1f;
            yield return new WaitForSeconds(.01f);
        }
    }

    public void OnCrouch(InputValue input)
    {
        StopAllCoroutines();

        if (input.isPressed)
        {
            Debug.Log("CROCUH PRESSED");
            fadeCrouchDown = FadeCrouchDown();
            StartCoroutine(fadeCrouchDown);
        } else
        {
            characterController.height = OriginalHeight;
            transform.position += transform.up * OriginalHeight/2;
        }
    }

    private void Awake()
    {
        Cursor.lockState = (LockCursorOnStart) ? CursorLockMode.Locked : CursorLockMode.None;
        OriginalHeight = characterController.height;

        fadeCrouchDown = FadeCrouchDown();
        uiCon = GameObject.FindGameObjectWithTag("UIController").GetComponent<UI_Controller>();
    }

    private void FixedUpdate()
    {
        //falling/jumping
        characterController.Move(transform.up * Time.deltaTime * ClampAbsolute(verticalVelocity, MaxFallSpeed));
        //horizontal movement
        characterController.Move((transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * MoveSpeed * Time.deltaTime);

        //MOVEMENTS
        isGrounded = Physics.CheckSphere(transform.position, characterController.height / 2 + 0.1f, GroundLayerMask);
        if (isGrounded)
        {
            if (isJumping)
            {
                verticalVelocity = Mathf.Sqrt(2f * JumpHeight * GravityAcceleration);
                isJumping = false;
            }
            else
            {
                verticalVelocity = 0;
            }
        }
        else
        {
            verticalVelocity += GravityAcceleration * Time.deltaTime * -1;
        }
    }

    private void Update()
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


    }

    #region UI DIALOGUE SELECTION
    private void OnDialogue1()
    {
        uiCon.SelectDialogueOption(1);
    }
    private void OnDialogue2()
    {
        uiCon.SelectDialogueOption(2);
    }
    private void OnDialogue3()
    {
        uiCon.SelectDialogueOption(3);
    }
    private void OnDialogue4()
    {
        uiCon.SelectDialogueOption(4);
    }
    #endregion
}
