using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using static GraphSystem.GraphConnections;

namespace GraphSystem
{
    public class DialogueNode : Node
    {
        public struct DialogueData
        {
            public string name;
            public string dialog;
        }

        [SerializeField, TextArea] private string name;
        [SerializeField, TextArea(5, 100)] private string dialogue;

        private DialogueData dialogueData;

        [Input(dynamicPortList = true)]
        public ResponseConnection previous;

        [Output(dynamicPortList = true)]
        public List<ResponseConnection> responses;

        protected override void Init()
        {
            base.Init();

            dialogueData = new DialogueData();
            dialogueData.name = name;
            dialogueData.dialog = dialogue;
        }

        public override object GetValue(NodePort port)
        {
            return dialogueData;
        }
    }
}