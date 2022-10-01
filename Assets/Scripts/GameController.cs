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
    //[SerializeField] <UI_controller> UIController;

    [SerializeField] List<NamedTriggeredEvent> TriggeredEvents;

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
