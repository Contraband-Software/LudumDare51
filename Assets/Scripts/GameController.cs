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

    [Header("References")]
    [SerializeField] AbstractTriggeredDialogue MainDialogue;
    [SerializeField] List<NamedTriggeredEvent> TriggeredEvents;

    UI_Controller UIController;

    AbstractTriggeredDialogue currentEvent;
    NodeData currentDialogue;

    private void Start()
    {
        currentEvent = MainDialogue;
        UIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UI_Controller>();

        InitCurrentEvent();
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
            case NodeType.Root:
                break;
            case NodeType.Dialogue:

                Debug.Log("HandleCurrentNode NodeType.Dialogue");

                DialogueNode.DialogueData diagData = (DialogueNode.DialogueData)currentDialogue.data;

                bool canRespond = false;
                if (diagData.responses.Count == 1)
                {
                    canRespond = diagData.responses[0].response == GraphGlobals.MagicDialogueSkipValue;
                }

                UIController.DrawNode(diagData.dialog, canRespond, diagData.name, diagData.responses, diagData.timeOut);
                break;
            case NodeType.Wait:
                break;
            case NodeType.End:
                break;
        }
    }

    public void PostResponse(int choiceIndex)
    {

    }
    /*
     bool waiting = false;
    float timeTowait = 0;


     loadchronosequence
        thiscurrentnode = MainDialogue.initiate()
       
     update
            
            thisrunning = MainDialogue.isComplete()

            nodedelay = 0
                
            switch (data.type)
            {
                case NodeType.Root:
                    break;
                case NodeType.Dialogue:
                    DialogueNode.DialogueData diag = (DialogueNode.DialogueData)data.data;
                    uimanager.display(nodedata);
                    break;
                case NodeType.Wait:
                    float time = (float)data.data;
                    Debug.Log(time);
                    nodedelay = nodedata.data
                    break;
                case NodeType.End:
                    Debug.Log("The end");
                    break;
            }
     */
}
