using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MiniGames;
using UnityEngine.InputSystem;

public class InteractWithObjects : MonoBehaviour
{
    [Header("RAYCASTING")]
    [SerializeField] private GameObject raycastObject;
    [SerializeField] private float interactionDistance;
    [SerializeField] private CanvasGroup interactPrompt_cg;
    [SerializeField] private TextMeshProUGUI interactPromptText;
    [SerializeField] private LayerMask layerMask;
 
    private bool hoveringOverObject = false;
    private bool showingInteractionPrompt = false;


    private AbstractBaseObject currentlyHoveredObject;

    private List<InteractableObject> Inventory;

    [Header("HACKY AS SHIT BUT I CBA (LOCKPICK TASK)")]

    private bool nextInteractIsExit = false;
    public LockPickTask lockPickTask;


    // Start is called before the first frame update
    void Start()
    {
        interactPrompt_cg.alpha = 0f;

        Inventory = new List<InteractableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForRaycastCollisions();
    }


    private void CheckForRaycastCollisions()
    {
        Vector3 fwd = raycastObject.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(raycastObject.transform.position, fwd * interactionDistance, Color.green);
        RaycastHit objectHit;
        if (Physics.Raycast(raycastObject.transform.position, fwd, out objectHit, interactionDistance, layerMask))
        {
            if (objectHit.collider.gameObject.tag == "Interactable" || objectHit.collider.gameObject.tag == "Useable") 
            {
                GameObject root = objectHit.collider.transform.parent.gameObject;
                hoveringOverObject = true;
                if(currentlyHoveredObject == null || root.GetInstanceID() != currentlyHoveredObject.gameObject.GetInstanceID())
                {
                    currentlyHoveredObject = root.GetComponent<AbstractBaseObject>();
                    DisplayInteractionPrompt(currentlyHoveredObject.GetObjectName());
                }
            }
            else
            {
                hoveringOverObject = false;
                currentlyHoveredObject = null;
                if (showingInteractionPrompt)
                {
                    HideInteractionPrompt();
                }
            }
        }
        else
        {
            hoveringOverObject = false;
            currentlyHoveredObject = null;
            if (showingInteractionPrompt)
            {
                HideInteractionPrompt();
            }
        }
    }

    public void NextInteractIsExit()
    {
        nextInteractIsExit = true;
    }

    private void DisplayInteractionPrompt(string objectName)
    {
        showingInteractionPrompt = true;

        interactPrompt_cg.alpha = 1f;
        interactPromptText.text = objectName + " - Press [E] To Interact";
        print("DISPLAYING PROMPT");
    }
    public void HideInteractionPrompt()
    {
        showingInteractionPrompt = false;
        interactPrompt_cg.alpha = 0f;
        print("HIDING PROMPT");
    }

    //InputSystem Message
    private void OnInteract(InputValue input)
    {
        if (nextInteractIsExit)
        {
            if (lockPickTask.taskDisplaying)
            {
                lockPickTask.HideLockPick(); ;
            }
            nextInteractIsExit = false;
            return;
        }

        if (currentlyHoveredObject != null)
        {
            currentlyHoveredObject.OnInteract.Invoke();
            nextInteractIsExit = false;

            if (currentlyHoveredObject.gameObject.tag == "Interactable")
            {
                Debug.Log(((InteractableObject)currentlyHoveredObject).GetObjectName());

                Inventory.Add((InteractableObject)currentlyHoveredObject);

                //Make gameObject go away
                foreach (Transform child in currentlyHoveredObject.transform)
                {
                    child.gameObject.SetActive(false);
                }
            } else {
                //Useable
                UseableObject station = (UseableObject)currentlyHoveredObject;
                station.Use(Inventory);
            }
        }

        Debug.Log(Inventory.Count);
    }

    private void OnMinigameInteract()
    {
        if(lockPickTask != null && !lockPickTask.taskComplete && lockPickTask.taskDisplaying)
        {
            lockPickTask.LockPickPlace();
        }
    }
}
