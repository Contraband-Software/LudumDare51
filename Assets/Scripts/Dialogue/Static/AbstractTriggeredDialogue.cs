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
        public abstract NodeData Initiate();
        public abstract NodeData AdvanceDialogue(int responseIndex);
        public abstract NodeData GetCurrentDialogue();
        public abstract bool IsComplete();
    }
}