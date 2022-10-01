using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace GraphSystem
{
    public abstract class AbstractTriggeredDialogue : MonoBehaviour
    {
        [SerializeField] NodeGraph graph;

        protected Parser DialogueGraphParser;

        private void Awake()
        {
            DialogueGraphParser = new Parser(graph);
        }

        public abstract void Suspend(bool status);
        public abstract BaseNode Initiate();
        public abstract BaseNode AdvanceDialogue(int responseIndex);
        public abstract bool IsComplete();
    }
}