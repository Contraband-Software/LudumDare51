using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UI_Controller : MonoBehaviour
{

    [Header("Dialogue Display")]
    [SerializeField] CanvasGroup dialogueUI_cg;
    [SerializeField] CanvasGroup dialogueOptions_cg;
    [SerializeField] GameObject transitionedTexts;
    [SerializeField] TextMeshProUGUI speakerName_text;
    [SerializeField] List<TextMeshProUGUI> dialogueOptions_text = new List<TextMeshProUGUI>();


    private List<string> current_DialogueOptions = new List<string>();
    private int diagOptions = 0;
    private string current_SpeakerName;

    private void Awake()
    {

    }


    //Pass in the speaker name and list of options
    //shows UI for the given data. Should be CALLED ONCE per dialogue change
    public void DrawNode(string speakerName, List<string> dialogueOptions)
    {
        MoveAllTransitionedBack();
        InstantSoftClearDialogueOptions();
        InstantDialoguePanelOpaque();

        current_SpeakerName = speakerName;
        current_DialogueOptions = dialogueOptions;
        diagOptions = dialogueOptions.Count;

        //UPDATE TEXTS TO REFLECT RESPONSE PROMPT
        speakerName_text.text = speakerName + ": ";
        float smallestTextSize = Mathf.Infinity;
        for(int s = 0; s < dialogueOptions.Count; s++)
        {
            dialogueOptions_text[s].text = (s+1).ToString() + "] " + dialogueOptions[s];
            if(dialogueOptions_text[s].fontSize < smallestTextSize)
            {
                smallestTextSize = dialogueOptions_text[s].fontSize;
            }
        }

        //UNIFORMIZE DIALOGUE OPTIONS FONT SIZES
        foreach(TextMeshProUGUI t in dialogueOptions_text)
        {
            t.enableAutoSizing = false;
            t.fontSize = smallestTextSize;
        }

    }

    //prompts a test dialogue
    private void OnDialogueTest()
    {
        DrawNode("Jakub", new List<string> { "How are you?", "I hate you", "Im hungy" });
    }

    #region DIALOGUE OPTION HANDLING (CLUNKY AS SHIT)
    private void OnDialogue1()
    {
        if(diagOptions >= 1)
        {
            SendDialogueChosen(1);
        }
    }
    private void OnDialogue2()
    {
        if (diagOptions >= 2)
        {
            SendDialogueChosen(2);
        }
    }
    private void OnDialogue3()
    {
        if (diagOptions >= 3)
        {
            SendDialogueChosen(3);
        }
    }
    private void OnDialogue4()
    {
        if (diagOptions >= 4)
        {
            SendDialogueChosen(4);
        }
    }
    #endregion

    private void SendDialogueChosen(int indexChosen)
    {
        print("OPTION CHOSE: " + indexChosen.ToString());
        ChosenDialogueTransition(indexChosen-1);
        FadeOutDialogueOptions();
    }




    #region DIALOGUE VISIBILITY CONTROLS

    //places chosen dialogue option into region where it wont fade out
    private void ChosenDialogueTransition(int indexChosen)
    {
        Transform rect = dialogueOptions_text[indexChosen].transform;
        rect.SetParent(rect.parent.parent.Find("DialogueOptions_Transitioned"));
    }

    private void MoveAllTransitionedBack()
    {
        for(int c = 0; c < transitionedTexts.transform.childCount; c++)
        {
            transitionedTexts.transform.GetChild(c).SetParent(dialogueOptions_cg.gameObject.transform);
        }
    }


    //changes dialogue option texts to be blank
    private void InstantSoftClearDialogueOptions()
    {
        foreach (TextMeshProUGUI t in dialogueOptions_text)
        {
            t.text = "";
        }
    }

    //forces dialogue panel to be alpha 1
    private void InstantDialoguePanelOpaque()
    {
        dialogueUI_cg.alpha = 1f;
        dialogueOptions_cg.alpha = 1f;
    }


    //fades out entire dialogue PANEL
    private void FadeOutDialoguePanel()
    {
        LeanTween.value(dialogueUI_cg.gameObject, UpdateEntireCGAlpha, 1f, 0f, 1f).setEase(LeanTweenType.easeOutQuad);
    }
    private void UpdateEntireCGAlpha(float a)
    {
        dialogueUI_cg.alpha = a;
    }

    //fades out dialogue OPTIONS
    private void FadeOutDialogueOptions()
    {
        LeanTween.value(dialogueOptions_cg.gameObject, UpdateOptionsCGAlpha, 1f, 0f, 1f).setEase(LeanTweenType.easeOutQuad);
    }
    private void UpdateOptionsCGAlpha(float a)
    {
        dialogueOptions_cg.alpha = a;
    }
    #endregion
}
