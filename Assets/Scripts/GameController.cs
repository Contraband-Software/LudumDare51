using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GraphSystem;

public class GameController : MonoBehaviour
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

    UI_Controller UIController;

    AbstractTriggeredDialogue currentEvent;
    NodeData currentDialogue;

    SequenceState currentSequenceState;

    private void MainSequenceEnd()
    {
        Debug.Log("MAIN SEQUENCE ENDED.");
    }

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

    private AbstractTriggeredDialogue GetNamedSequence(string name)
    {
        foreach (NamedTriggeredEvent namedTriggeredEvent in TriggeredEvents)
        {
            if (namedTriggeredEvent.Name == name)
            {
                return namedTriggeredEvent.EventController;
            }
        }

        throw new Exception("GAME CONTROLLER: NO TRIGGERED EVENT WITH THAT NAME");
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
            //case NodeType.Root:
            //    break;
            case NodeType.Dialogue:
                //Debug.Log("HandleCurrentNode NodeType.Dialogue");

                DialogueNode.DialogueData diagData = (DialogueNode.DialogueData)currentDialogue.data;

                bool canRespond = false;
                if (diagData.responses.Count == 1)
                {
                    canRespond = diagData.responses[0].response == GraphGlobals.MagicDialogueSkipValue;
                }

                UIController.DrawNode(diagData.dialog, canRespond, diagData.name, diagData.responses, diagData.timeOut);
                break;
            case NodeType.Wait:
                float waitData = (float)currentDialogue.data;
                //wait for time, then move to next node
                WaitNode(waitData);
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
        //-1 means there was no dialogue options
        if (choiceIndex == -1)
        {
            AdvanceDialogueChoiceless();
        } else
        {
            currentDialogue = currentEvent.AdvanceDialogue(choiceIndex);
        }
        HandleCurrentNode();
    }

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
}
