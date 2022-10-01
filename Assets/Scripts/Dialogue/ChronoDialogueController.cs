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
    public override BaseNode AdvanceDialogue(int responseIndex)
    {
        BaseNode newNode = DialogueGraphParser.GetNextNode((uint)responseIndex);

        currentNodeData = newNode.GetNodeValue();

        return newNode;
    }

    public override BaseNode Initiate()
    {
        BaseNode newNode = DialogueGraphParser.GetStartNode();

        currentNodeData = newNode.GetNodeValue();

        return newNode;
    }
}
