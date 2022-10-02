﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace GraphSystem
{
    public abstract class DialogueBaseNode : BaseNode
    {
        public class BaseDialogueData
        {
            public string name;
            public string dialog;
            public float timeOut;
        }

        [SerializeField, TextArea] protected string speakerName;
        [SerializeField, TextArea(5, 100)] protected string dialogue;
        [SerializeField] protected AudioClip audioClip;
        [SerializeField, Min(0)] protected float timeOut;

        [Input]
        public GraphConnections.ResponseConnectionLink previous;
    }
}