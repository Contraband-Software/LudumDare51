using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace GraphSystem
{
    public class WaitNode : Node
    {
        [SerializeField, TextArea] private int tineInSeconds;

        // Use this for initialization
        protected override void Init()
        {
            base.Init();

        }

        public override object GetValue(NodePort port)
        {
            return tineInSeconds;
        }
    }
}