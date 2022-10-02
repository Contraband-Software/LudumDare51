using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GraphSystem;
using static GraphSystem.DialogueNode;

public class DialogueSequenceController : MonoBehaviour
{
    [Serializable]
    public struct NamedTriggeredEvent
    {
        public string Name;
        public AbstractTriggeredDialogue EventController;
    }

    enum SequenceState
    {
        Main,
        Event
    }

    [Header("References")]
    [SerializeField] AbstractTriggeredDialogue MainDialogue;
    [SerializeField] List<NamedTriggeredEvent> TriggeredEvents;

    [Header("Settings")]
    [Tooltip("Time in seconds to insert between consecutive dialogues on top of any wait nodes.")]
    [SerializeField, Min(0)] float GlobalDialogueDelay = 10;

    UI_Controller UIController;

    AbstractTriggeredDialogue currentEvent;
    NodeData currentDialogue;

    SequenceState currentSequenceState;

    bool MainSequenceEnded = false;

    private void Start()
    {
        currentEvent = MainDialogue;
        currentSequenceState = SequenceState.Main;

        UIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UI_Controller>();

        InitCurrentEvent();
    }

    private void Update()
    {
        if (currentEvent.IsComplete())
        {
            switch (currentSequenceState)
            {
                case SequenceState.Event:
                    //switch back to main sequence
                    currentSequenceState = SequenceState.Main;
                    currentEvent = MainDialogue;
                    currentEvent.Suspend(false);
                    currentDialogue = currentEvent.GetCurrentDialogue();
                    break;
                case SequenceState.Main:
                    MainSequenceEnd();
                    break;
            }
        }
    }

    private void MainSequenceEnd()
    {
        if (!MainSequenceEnded)
        {
            Debug.Log("DialogueSequenceController: MAIN SEQUENCE ENDED.");
            MainSequenceEnded = true;
        }
    }

    private AbstractTriggeredDialogue GetNamedSequence(string name)
    {
        foreach (NamedTriggeredEvent namedTriggeredEvent in TriggeredEvents)
        {
            if (namedTriggeredEvent.Name == name)
            {
                return namedTriggeredEvent.EventController;
            }
        }

        throw new Exception("DialogueSequenceController: NO TRIGGERED EVENT WITH THAT NAME");
    }

    private void InitCurrentEvent()
    {
        currentDialogue = currentEvent.Initiate();
        HandleCurrentNode();
    }

    private void HandleCurrentNode()
    {
        switch (currentDialogue.type)
        {
            case NodeType.Dialogue:
                DialogueNode.DialogueData diagData = (DialogueNode.DialogueData)currentDialogue.data;

                UIController.DrawNode(diagData.dialog, false, diagData.name, new List<GraphConnections.ResponseConnectionData>(), diagData.timeOut);
                break;
            case NodeType.DialogueRespond:
                DialogueNodeRespond.DialogueRespondData diagRespondData = (DialogueNodeRespond.DialogueRespondData)currentDialogue.data;

                UIController.DrawNode(diagRespondData.dialog, true, diagRespondData.name, diagRespondData.responses, diagRespondData.timeOut);
                break;
            case NodeType.Wait:
                WaitNode.WaitData waitData = (WaitNode.WaitData)currentDialogue.data;
                //wait for time, then move to next node
                //print(waitData.clearScreen);
                UIController.OnWait(waitData.timeInSeconds, waitData.clearScreen);

                StartCoroutine(WaitNode(waitData.timeInSeconds));
                break;
            case NodeType.End:
                //Stop processing nodes, continue processing custom evenmt script
                break;
        }
    }

    IEnumerator WaitNode(float time)
    {
        yield return new WaitForSeconds(time);

        AdvanceDialogueChoiceless();
        HandleCurrentNode();

        yield break;
    }

    private void AdvanceDialogueChoiceless()
    {
        currentDialogue = currentEvent.AdvanceDialogue(0);
    }

    public void PostResponse(int choiceIndex)
    {
        //Debug.Log(currentDialogue)
        //-1 means there was no dialogue options
        if (choiceIndex == -1)
        {
            AdvanceDialogueChoiceless();
        }
        else
        {
            currentDialogue = currentEvent.AdvanceDialogue(choiceIndex);
        }

        HandleCurrentNode();
    }

    /// <summary>
    /// Attempts to play a registered Dialogue Event Sequence, you may only do so if there isn't one already playing.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Whether it was successful or not</returns>
    public bool TryPlaySequence(string name)
    {
        if (currentSequenceState == SequenceState.Main)
        {
            currentEvent.Suspend(true);

            currentSequenceState = SequenceState.Event;
            currentEvent = GetNamedSequence(name);
            InitCurrentEvent();

            return true;
        }

        //triggered event already playing
        return false;
    }

    /// <summary>
    /// Returns whether the main dialogue sequence has ended or not
    /// </summary>
    /// <returns></returns>
    public bool GetMainSequenceStatus()
    {
        return MainSequenceEnded;
    }

    public float GetGlobalTimeDelay()
    {
        return GlobalDialogueDelay;
    }
}
