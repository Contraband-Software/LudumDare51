using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    DialogueSequenceController dialogueController;

    private DialogueSequenceController FindDialogueController()
    {
        return GameObject.FindGameObjectWithTag("ActController").GetComponent<DialogueSequenceController>();
    }

    private void Awake()
    {
        dialogueController = FindDialogueController();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dialogueController = FindDialogueController();
    }

    public DialogueSequenceController GetDialogueController()
    {
        return dialogueController;
    }
}
