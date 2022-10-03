using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickObject : MonoBehaviour
{
    public void OnInteract()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetDialogueController().PostFlag("BrickGet");
    }
}
