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
            public float timeOut;
            public List<GraphConnections.ResponseConnectionData> responses;
        }

        [SerializeField, TextArea] private string name;
        [SerializeField, TextArea(5, 100)] private string dialogue;
        [SerializeField, Min(0)] private float timeOut;

        private DialogueData dialogueData;

#region NODE_INTERFACE
        [Input]
        public GraphConnections.ResponseConnectionLink previous;

        [Output(dynamicPortList = true, connectionType = ConnectionType.Override)]
        public List<GraphConnections.ResponseConnectionData> responses;
#endregion

        protected override void Init()
        {
            base.Init();

            dialogueData = new DialogueData();
            dialogueData.name = name;
            dialogueData.dialog = dialogue;
            dialogueData.responses = responses;
            dialogueData.timeOut = timeOut;
            //dialogueData.responsesLength = responses.Count;
        }

        public override NodeData GetNodeValue()
        {
            NodeData baseData;
            baseData.type = NodeType.Dialogue;
            baseData.data = dialogueData;
            baseData.NodeID = "Dialig_" + name + "_" + this.position;
            return baseData;
        }
    }
}