using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Linq;
using XNode;

namespace GraphSystem
{
    using XNode;
    public class Parser
    {
        private NodeGraph graph;
        private RootNode root;
        private BaseNode currentNode;

        public Parser(NodeGraph chronoGraph)
        {
            graph = chronoGraph;
            root = GetRootNode(chronoGraph);
            currentNode = root.GetStartNode();

#if UNITY_EDITOR
            if (root == null)
            {
                Debug.LogException(new System.Exception("GraphSystem: NO ROOT NODE IN THIS GRAPH:" + graph.name));
            }
#endif
        }

        private RootNode GetRootNode(NodeGraph graph)
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

        /// <summary>
        /// Returns the node **after** the root node, the first game node.
        /// </summary>
        /// <returns></returns>
        public BaseNode GetStartNode()
        {
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
                        return (BaseNode)currentNode.DynamicOutputs.ToList<NodePort>()[(int)responseIndex].Connection.node;
                    }
                    else {
                        Debug.LogException(new System.Exception("Graph System: Response index out of range!"));
                        return null;
                    }
                    break;
                case NodeType.Wait:
                    return (BaseNode)currentNode.GetOutputPort(GraphGlobals.WaitNextFieldName).Connection.node;
                    break;
                case NodeType.End:
                case NodeType.Root:
                    return null;
                    break;
            }

            return null;
        }
    }
}