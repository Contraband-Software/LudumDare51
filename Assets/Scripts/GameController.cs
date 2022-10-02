using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueSequenceController))]
public class GameController : MonoBehaviour
{
    DialogueSequenceController dialogueController;

    private void Awake()
    {
        dialogueController = GetComponent<DialogueSequenceController>();
    }

    public DialogueSequenceController GetDialogueController()
    {
        return dialogueController;
    }
}
