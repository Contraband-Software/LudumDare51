using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XNode;

namespace GraphSystem
{
	public class ActionNode : BaseNode
	{
        [SerializeField] private string gameObject;

        [Input]
        public GraphConnections.ResponseConnectionLink previous;

        [Output(connectionType = ConnectionType.Override)]
        public GraphConnections.ResponseConnectionLink next;

        public override NodeData GetNodeValue()
		{
            NodeData baseData;
            baseData.type = NodeType.Action;
            baseData.data = gameObject;
            baseData.NodeID = "ActionNode_" + gameObject + "_" + this.position;
            return baseData;
        }
	}
}