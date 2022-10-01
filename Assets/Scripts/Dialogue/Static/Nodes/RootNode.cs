using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using static GraphSystem.DialogueNode;
using static GraphSystem.GraphGlobals;

namespace GraphSystem
{
    public class RootNode : BaseNode
    {
        [Header("Only use one RootNode!")]

        [Output(connectionType = ConnectionType.Override)]
        public GraphConnections.ResponseConnectionLink Start;

        public BaseNode GetStartNode()
        {
            return (BaseNode)GetOutputPort("Start").Connection.node;
        }

        public override NodeData GetNodeValue()
        {
            NodeData baseData;
            baseData.type = NodeType.Root;
            baseData.data = null;
            return baseData;
        }
    }
}