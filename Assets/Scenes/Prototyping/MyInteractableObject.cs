using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInteractableObject : MonoBehaviour
{
    public void Interact()
    {
        if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetDialogueController().TryPlaySequence("KeyDialogue"))
        {

        }

        //start game ui
    }
}
