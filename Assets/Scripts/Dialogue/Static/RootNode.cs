using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace GraphSystem
{
    public class RootNode : Node
    {
        [Output]
        public GraphConnections.ResponseConnection Start;

        protected override void Init()
        {
            base.Init();
        }

        public override object GetValue(NodePort port)
        {
            return Start;
        }
    }
}