using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObjects : MonoBehaviour
{
    [Header("RAYCASTING")]
    [SerializeField] private GameObject raycastObject;
    [SerializeField] private float interactionDistance;

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
                print("HITTING INTERACTABLE");
            }
        }
    }
}
