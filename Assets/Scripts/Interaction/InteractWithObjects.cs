using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObjects : MonoBehaviour
{
    [Header("RAYCASTING")]
    [SerializeField] private GameObject raycastObject;
    [SerializeField] private float interactionDistance;

    private string currentlyHoveredObject;
    private bool hoveringOverObject = false;
    private bool showingInteractionPrompt = false;


    // Start is called before the first frame update
    void Start()
    {
        
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
                    DisplayInteractionPrompt();
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

    private void DisplayInteractionPrompt()
    {
        showingInteractionPrompt = true;
        print("DISPLAYING PROMPT");
    }
    private void HideInteractionPrompt()
    {
        showingInteractionPrompt = false;
        print("HIDING PROMPT");
    }
}
