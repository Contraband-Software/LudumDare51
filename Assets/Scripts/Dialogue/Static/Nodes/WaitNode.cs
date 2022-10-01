using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using static GraphSystem.DialogueNode;
using static GraphSystem.GraphGlobals;

namespace GraphSystem
{
    public class WaitNode : BaseNode
    {
        [SerializeField] private float timeInSeconds;

        [Input]
        public GraphConnections.ResponseConnectionLink previous;

        [Output(connectionType = ConnectionType.Override)]
        public GraphConnections.ResponseConnectionLink next;

        public override NodeData GetNodeValue()
        {
            NodeData baseData;
            baseData.type = NodeType.Wait;
            baseData.data = timeInSeconds;
            baseData.NodeID = "WaitNode_" + timeInSeconds.ToString() + "_" + this.position;
            return baseData;
        }
    }
}