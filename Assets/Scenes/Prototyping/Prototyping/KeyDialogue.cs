using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphSystem;

public class KeyDialogue : AbstractTriggeredDialogue
{
    public override bool IsComplete()
    {
        return EndAtTerminator();
    }

    public override void Suspend(bool status)
    {

    }
}
