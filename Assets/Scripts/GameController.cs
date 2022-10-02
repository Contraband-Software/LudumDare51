using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-5)]
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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.ToString());
        Debug.Log(dialogueController == null);
        dialogueController = FindDialogueController();
    }

    public DialogueSequenceController GetDialogueController()
    {
        return dialogueController;
    }
}
