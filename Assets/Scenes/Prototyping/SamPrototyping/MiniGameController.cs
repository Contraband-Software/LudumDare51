using GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameController : GraphSystem.AbstractTriggeredDialogue
{
    public void StartEvent()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetDialogueController().TryPlaySequence("Button");
    }

    public void EndPress()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetDialogueController().PostFlag("upset");
        buttonPressed = true;
    }

    bool buttonPressed = false;

    private void Start()
    {
        GetComponent<GraphSystem.ActionComponent>().action = Meth;
    }

    public override void Suspend(bool status) { }

    /*public override NodeData Initiate()
    {
        BaseNode newNode = DialogueGraphParser.GetStartNode();

        currentNodeData = newNode.GetNodeValue();

        return GetCurrentDialogue();
    }

    public override NodeData AdvanceDialogue(int responseIndex)
    {
        BaseNode newNode = DialogueGraphParser.GetNextNode((uint)responseIndex);
        currentNodeData = newNode.GetNodeValue();

        return GetCurrentDialogue();
    }

    public override NodeData GetCurrentDialogue()
    {
        return currentNodeData;
    }*/

    public override bool IsComplete()
    {
        return buttonPressed;
    }

    public void Meth()
    {
        Debug.Log("Before goodbye");
    }
}
