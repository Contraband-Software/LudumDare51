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
            Debug.Log("AbstractTriggeredDialogue awake");
            DialogueGraphParser = new Parser(graph);
        }

        public abstract void Suspend(bool status);
        public abstract NodeData Initiate();
        public abstract NodeData AdvanceDialogue(int responseIndex);
        public abstract bool IsComplete();
    }
}