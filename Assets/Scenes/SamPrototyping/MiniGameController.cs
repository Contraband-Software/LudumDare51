using GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameController : MonoBehaviour//GraphSystem.AbstractTriggeredDialogue
{
    private void Start()
    {
        GetComponent<GraphSystem.ActionComponent>().action = Meth;
    }

    //public override void Suspend(bool status)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public override NodeData Initiate()
    //{
    //    throw new System.NotImplementedException();
    //}

    //public override NodeData AdvanceDialogue(int responseIndex)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public override NodeData GetCurrentDialogue()
    //{
    //    throw new System.NotImplementedException();
    //}

    //public override bool IsComplete()
    //{
    //    throw new System.NotImplementedException();
    //}














    public void Meth()
    {
        Debug.Log("Before goodbye");
    }
}
