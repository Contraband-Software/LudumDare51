using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphSystem
{
    public abstract class AbstractTriggeredDialogue : MonoBehaviour
    {
        [SerializeField] EventGraph graph;

        private Parser DialogueGraphParser;

        private void Awake()
        {
            DialogueGraphParser = new Parser(graph);
        }

        public abstract void OnComplete();
    }
}