using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using GraphSystem;

public class DialogueParserController : MonoBehaviour
{
    [SerializeField] NodeGraph graph;

    private GraphSystem.Parser Parser;

    void Start()
    {
        Parser = new GraphSystem.Parser((ChronoDialogueGraph)graph);

        NodeData data = Parser.GetStartNode().GetNodeValue();

        switch (data.type)
        {
            case NodeType.Root:
                break;
            case NodeType.Dialogue:
                DialogueNode.DialogueData diag = (DialogueNode.DialogueData)data.data;
                Debug.Log(diag.dialog);
                Parser.GetNextNode(2);
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