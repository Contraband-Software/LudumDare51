

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperClipObject : MonoBehaviour
{
    [SerializeField] GameObject note;

    public void OnPickupClip()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetDialogueController().PostFlag("Shower");
        
        Destroy(note);
    }
}
