using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XNode;
using UnityEngine.Events;
using static GraphSystem.WaitNode;

namespace GraphSystem
{
	public class ActionNode : AbstractOneToOneNode
    {
        public struct ActionData
        {
            public UnityEvent unityEvent;
        }

        [SerializeField] UnityEvent unityEvent;

        private ActionData actionData;

        protected override void Init()
        {
            base.Init();

            actionData = new ActionData();
            actionData.unityEvent = unityEvent;
        }

        public override NodeData GetNodeValue()
		{
            NodeData baseData;
            baseData.type = NodeType.Action;
            baseData.data = actionData;
            baseData.NodeID = "ActionNode_" + unityEvent.ToString() + "_" + this.position;
            return baseData;
        }
	}
}