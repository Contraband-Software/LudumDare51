using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphSystem
{
    public enum NodeType
    {
        Root,
        Wait,
        Dialogue,
        End
    }
    public struct NodeData
    {
        public NodeType type;
        public object data;
    }

    public static class GraphGlobals
    {
        public static string ResponseFieldName = "Responses";
        public static string WaitNextFieldName = "Next";
    }
}