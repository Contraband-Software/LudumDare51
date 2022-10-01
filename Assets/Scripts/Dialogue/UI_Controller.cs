using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UI_Controller : MonoBehaviour
{

    [Header("Dialogue Display")]
    [SerializeField] CanvasGroup dialogueUI_cg;
    [SerializeField] TextMeshProUGUI speakerName_text;
    [SerializeField] List<TextMeshProUGUI> dialogueOptions_text = new List<TextMeshProUGUI>();

    private void Awake()
    {

    }


    //Pass in the speaker name and list of options
    //shows UI for the given data. Should be CALLED ONCE per dialogue change
    public void DrawNode(string speakerName, List<string> dialogueOptions)
    {
        speakerName_text.text = speakerName + ": ";
        for(int s = 0; s < dialogueOptions.Count; s++)
        {
            dialogueOptions_text[s].text = s.ToString() + "] " + dialogueOptions[s];
        }

    }


    private void OnDialogueTest()
    {
        print("P pressed");

        DrawNode("Jakub", new List<string> { "How are you?", "I hate you", "Im hungy" });
    }
}
