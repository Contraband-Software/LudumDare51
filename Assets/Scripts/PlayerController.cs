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
    [SerializeField, Min(0)] float CrouchSpeed = 0.1f;
    [SerializeField, Min(0)] float CrouchCutOffValue = 0.1f;

    [Header("Camera")]
    [SerializeField] float SensitivityX = 8f;
    [SerializeField] float SensitivityY = 0.5f;
    [SerializeField] bool LockCursorOnStart = true;
    [SerializeField] Transform PlayerCamera;
    [SerializeField, Min(0)] float LookXClamp = 85f;

    [Header("Environment")]
    [SerializeField] float GravityAcceleration = 30.0f;
    [SerializeField] LayerMask GroundLayerMask;

    [Header("Debug")]
    [SerializeField] bool UseUIController = true;


    private UI_Controller uiCon;

    private Vector2 horizontalInput;

    private float verticalVelocity = 0f;

    private bool isGrounded = false;
    private bool isJumping = false;

    private float mouseX, mouseY;
    private float xRotation = 0f;

    private float OriginalHeight;

    private Transform freezeTransform;
    private bool isFreeze = false;
    IEnumerator fadeCrouchDown;
    IEnumerator fadeCrouchUp;

    private float ClampAbsolute(float val, float factor) { return Mathf.Clamp(val, factor * -1, factor); }

    public void OnMove(InputValue input)
    {
        if (!isFreeze)
        {
            horizontalInput = input.Get<Vector2>();

        }
        
    }

    public void OnLook(InputValue input)
    {
        if (!isFreeze)
        {
            mouseX = input.Get<Vector2>().x;
            mouseY = input.Get<Vector2>().y;
        }
        
    }

    public void OnJump(InputValue input)
    {
        isJumping = true;
    }

    public void Freeze()
    {
        isFreeze = true;
        horizontalInput = Vector2.zero;
        mouseX = 0f;
        mouseY = 0f;
    }

    public void UnFreeze()
    {
        isFreeze=false;
    }

    IEnumerator FadeCrouchDown()
    {
        Debug.Log("START CROUCH");
        while (characterController.height - OriginalHeight * CrouchHeightPercentage > CrouchCutOffValue)
        {
            characterController.height -= (characterController.height - OriginalHeight * CrouchHeightPercentage) * CrouchSpeed;
            yield return new WaitForSeconds(.001f);
        }
    }
    IEnumerator FadeCrouchUp()
    {
        Debug.Log("END CROUCH");
        while (OriginalHeight - characterController.height > CrouchCutOffValue)
        {
            characterController.height += (OriginalHeight - characterController.height) * CrouchSpeed;
            yield return new WaitForSeconds(.001f);
        }

        //transform.position += transform.up * OriginalHeight / 2;
    }

    public void OnCrouch(InputValue input)
    {

        if (!isFreeze)
        {
            StopAllCoroutines();

            if (input.isPressed)
            {
                Debug.Log("CROCUH PRESSED");
                fadeCrouchDown = FadeCrouchDown();
                StartCoroutine(fadeCrouchDown);
            }
            else
            {
                fadeCrouchUp = FadeCrouchUp();
                StartCoroutine(fadeCrouchUp);
            }
        }
        
    }

    private void Awake()
    {
        Cursor.lockState = (LockCursorOnStart) ? CursorLockMode.Locked : CursorLockMode.None;
        OriginalHeight = characterController.height;

        fadeCrouchDown = FadeCrouchDown();

        if (UseUIController)
        {
            uiCon = GameObject.FindGameObjectWithTag("UIController").GetComponent<UI_Controller>();
        }
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
