using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GraphSystem;

public class ChronoDialogueController : AbstractTriggeredDialogue
{
    [Header("Settings")]
    [Tooltip("Time in seconds to insert between consecutive dialogues on top of any wait nodes.")]
    [SerializeField, Min(0)] float GlobalDialogueDelay = 10;

    NodeData currentNodeData;

    public override void Suspend(bool status)
    {

    }

    public override bool IsComplete()
    {
        //stop coroutine, start scene-end sequence

        return false;
    }
    public override NodeData AdvanceDialogue(int responseIndex)
    {
        BaseNode newNode = DialogueGraphParser.GetNextNode((uint)responseIndex);

        currentNodeData = newNode.GetNodeValue();

        return currentNodeData;
    }

    public override NodeData Initiate()
    {
        BaseNode newNode = DialogueGraphParser.GetStartNode();

        currentNodeData = newNode.GetNodeValue();

        Debug.Log("ChronoDialogueController Initiate");

        return currentNodeData;
    }
}
