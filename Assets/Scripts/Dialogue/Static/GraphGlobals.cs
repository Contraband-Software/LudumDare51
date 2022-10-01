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
        Action,
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
        public static string LinkNextNodeFieldName = "Next";

        public static string MagicDialogueSkipValue = "[PASS]";
    }
}