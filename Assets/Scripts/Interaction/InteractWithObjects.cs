using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractWithObjects : MonoBehaviour
{
    [Header("RAYCASTING")]
    [SerializeField] private GameObject raycastObject;
    [SerializeField] private float interactionDistance;
    [SerializeField] private CanvasGroup interactPrompt_cg;
    [SerializeField] private TextMeshProUGUI interactPromptText;

    private string currentlyHoveredObject;
    private bool hoveringOverObject = false;
    private bool showingInteractionPrompt = false;


    // Start is called before the first frame update
    void Start()
    {
        interactPrompt_cg.alpha = 0f;
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
        if (Physics.Raycast(raycastObject.transform.position, fwd, out objectHit, interactionDistance))
        {
            if (objectHit.collider.gameObject.tag == "Interactable")
            {
                hoveringOverObject = true;
                if(objectHit.collider.gameObject.name != currentlyHoveredObject)
                {
                    currentlyHoveredObject = objectHit.collider.gameObject.name;
                    DisplayInteractionPrompt(objectHit.collider.gameObject.name);
                }
            }
            else
            {
                hoveringOverObject = false;
                currentlyHoveredObject = "";
                if (showingInteractionPrompt)
                {
                    HideInteractionPrompt();
                }
            }
        }
        else
        {
            hoveringOverObject = false;
            currentlyHoveredObject = "";
            if (showingInteractionPrompt)
            {
                HideInteractionPrompt();
            }
        }
    }

    private void DisplayInteractionPrompt(string objectName)
    {
        showingInteractionPrompt = true;
        interactPrompt_cg.alpha = 1f;
        interactPromptText.text = objectName + " - Press [E] To Interact";
        print("DISPLAYING PROMPT");
    }
    private void HideInteractionPrompt()
    {
        showingInteractionPrompt = false;
        interactPrompt_cg.alpha = 0f;
        print("HIDING PROMPT");
    }
}
