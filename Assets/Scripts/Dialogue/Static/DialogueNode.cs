using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using static GraphSystem.GraphGlobals;

namespace GraphSystem
{
    public class DialogueNode : BaseNode
    {
        public struct DialogueData
        {
            public string name;
            public string dialog;
            public List<GraphConnections.ResponseConnection> responses;
            public int responsesLength;
        }

        [SerializeField, TextArea] private string name;
        [SerializeField, TextArea(5, 100)] private string dialogue;

        private DialogueData dialogueData;

        [Input]
        public GraphConnections.ResponseConnectionLink previous;

        [Output(dynamicPortList = true)]
        public List<GraphConnections.ResponseConnectionData> responses;

        protected override void Init()
        {
            base.Init();

            dialogueData = new DialogueData();
            dialogueData.name = name;
            dialogueData.dialog = dialogue;
            //dialogueData.responses = responses;
            //dialogueData.responsesLength = responses.Count;
        }

        public override NodeData GetNodeValue()
        {
            NodeData baseData;
            baseData.type = NodeType.Dialogue;
            baseData.data = dialogueData;
            return baseData;
        }
    }
}