using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphSystem;

public class KeyDialogue : AbstractTriggeredDialogue
{
    public override bool IsComplete()
    {
        return GetCurrentDialogue().type == NodeType.End;
    }

    public override void Suspend(bool status)
    {

    }
}
