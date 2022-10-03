using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRDialogue : GraphSystem.AbstractTriggeredDialogue
{
    public override bool IsComplete()
    {
        return EndAtTerminator();
    }

    public override void Suspend(bool status)
    {
        throw new System.NotImplementedException();
    }
}
