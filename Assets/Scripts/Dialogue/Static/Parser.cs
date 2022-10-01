using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Linq;

namespace GraphSystem
{
    using XNode;
    public class Parser
    {
        private ChronoDialogueGraph graph;
        private RootNode root;
        private BaseNode currentNode;

        public Parser(ChronoDialogueGraph chronoGraph)
        {
            graph = chronoGraph;
            root = GetRootNode(chronoGraph);

#if UNITY_EDITOR
            if (root == null)
            {
                Debug.LogException(new System.Exception("GraphSystem: NO ROOT NODE IN THIS GRAPH:" + graph.name));
            }
#endif
        }

        private RootNode GetRootNode(ChronoDialogueGraph graph)
        {
            foreach (BaseNode node in graph.nodes)
            {
                if (node is RootNode)
                {
                    return (RootNode)node;
                }
            }

            return null;
        }

        public BaseNode GetStartNode()
        {
            currentNode = root.GetStartNode();
            return currentNode;
        }

        public BaseNode GetNextNode(uint responseIndex)
        {
            NodeType currentNodeType = currentNode.GetNodeValue().type;

            switch (currentNodeType)
            {
                case NodeType.Dialogue:
                    if (responseIndex < currentNode.DynamicOutputs.Count())
                    {
                        //currentNode.DynamicOutputs
                    }
                    //foreach (NodePort dynamicOutput in currentNode.DynamicOutputs)
                    //{
                    //    Debug.Log(dynamicOutput.fieldName);
                    //}
                    break;
                case NodeType.Wait:
                    break;
            }

            return null;
            
            //currentNode is not a dialogueNode
        }
    }
}