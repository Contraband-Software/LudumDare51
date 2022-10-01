using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace GraphSystem
{
	public class TerminatorNode : BaseNode
	{
        [Input]
        public GraphConnections.ResponseConnectionLink previous;

        public override NodeData GetNodeValue()
        {
            NodeData baseData;
            baseData.type = NodeType.End;
            baseData.data = null;
            return baseData;
        }
    }
}