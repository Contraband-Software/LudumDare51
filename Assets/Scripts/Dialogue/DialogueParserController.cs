using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using GraphSystem;

/*
 
TEST SCRIPT, NOT PRODUCTION
 
 */

public class DialogueParserController : MonoBehaviour
{
    [SerializeField] NodeGraph graph;

    private GraphSystem.Parser Parser;

    void Start()
    {
        Parser = new GraphSystem.Parser(graph);

        NodeData data = Parser.GetStartNode().GetNodeValue();

        switch (data.type)
        {
            case NodeType.Root:
                break;
            case NodeType.Dialogue:
                DialogueNode.DialogueData diag = (DialogueNode.DialogueData)data.data;
                Debug.Log(diag.dialog);
                NodeData data2 = Parser.GetNextNode(0).GetNodeValue();
                DialogueNode.DialogueData diag2 = (DialogueNode.DialogueData)data2.data;
                Debug.Log(diag2.dialog);
                break;
            case NodeType.Wait:
                float time = (float)data.data;
                Debug.Log(time);
                break;
            case NodeType.End:
                Debug.Log("The end");
                break;
        }
    }
}
