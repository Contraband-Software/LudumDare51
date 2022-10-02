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
        DialogueRespond,
        HaltFlagged,
        Action,
        End
    }
    public struct NodeData
    {
#if UNITY_EDITOR
        public string NodeID;
#endif
        public NodeType type;
        public object data;
    }

    public static class GraphGlobals
    {
        public static string ResponseFieldName = "Responses";
        public static string LinkNextNodeFieldName = "next";

        public static string MagicDialogueSkipValue = "[PASS]";
    }
}