using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-5)]
public class GameController : MonoBehaviour
{
    DialogueSequenceController dialogueController;

    private void FindDialogueController()
    {
        try
        {
            dialogueController = GameObject.FindGameObjectWithTag("ActController").GetComponent<DialogueSequenceController>();
        } catch {

        }
    }

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        FindDialogueController();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindDialogueController();
    }

    public DialogueSequenceController GetDialogueController()
    {
        return dialogueController;
    }
}
