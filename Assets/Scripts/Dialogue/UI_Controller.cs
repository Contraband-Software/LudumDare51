using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Controller : MonoBehaviour
{
    //[SerializeField] InputAction dialogueTestInput;
    [SerializeField] CanvasGroup dialogueUI_cg;

    private void Awake()
    {

    }


    //Pass in the speaker name and list of options
    //shows UI for the given data. Should be CALLED ONCE per dialogue change
    public void DrawNode(string speakerName, List<string> dialogueOptions)
    {

    }


    private void OnDialogueTest()
    {
        print("P pressed");
    }
}
