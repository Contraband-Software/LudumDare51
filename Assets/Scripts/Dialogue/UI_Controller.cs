using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

using GraphSystem;

public class UI_Controller : MonoBehaviour
{

    [Header("Dialogue Display")]
    [SerializeField] CanvasGroup dialogueUI_cg;
    [SerializeField] CanvasGroup dialogueOptions_cg;
    [SerializeField] GameObject transitionedTexts;
    [SerializeField] TextMeshProUGUI speakerName_text;
    [SerializeField] List<TextMeshProUGUI> dialogueOptions_text = new List<TextMeshProUGUI>();


    [Header("UI Animation")]
    //Resetting Variables
    private bool dialogueChosen = false;
    [SerializeField] List<RectTransform> dialogueTextRects = new List<RectTransform>();
    private Vector2 option1_position;
    private List<Vector2> dialogueTextPositionsStored = new List<Vector2>();
    [SerializeField] LeanTweenType cg_fadout;
    [SerializeField] float cg_fadout_time;

    //Dialogue Variables
    private List<GraphConnections.ResponseConnectionData> current_DialogueOptions = new List<GraphConnections.ResponseConnectionData>();
    private int diagOptions = 0;
    private string current_SpeakerName;

    //Game Controls
    GameController gameCon;

    private void Awake()
    {
        gameCon = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        SetOriginalTextPositions();
    }


    //Pass in the speaker name and list of options
    //shows UI for the given data. Should be CALLED ONCE per dialogue change
    public void DrawNode(string incomingText, bool canReply, string speakerName, List<GraphConnections.ResponseConnectionData> dialogueOptions, float timeOut)
    {
        Debug.Log("DrawNode");

        MoveAllTransitionedBack();
        InstantSoftClearDialogueOptions();
        InstantDialoguePanelOpaque();
        ResetReadyToDisplay();

        current_SpeakerName = speakerName;
        current_DialogueOptions = dialogueOptions;
        diagOptions = dialogueOptions.Count;

        int autoChooseChoice = 0;

        //UPDATE TEXTS TO REFLECT RESPONSE PROMPT
        speakerName_text.text = speakerName + ": ";
        float smallestTextSize = Mathf.Infinity;
        for(int s = 0; s < dialogueOptions.Count; s++)
        {
            if (dialogueOptions[s].AutoChoose)
            {
                autoChooseChoice = s;
                Debug.Log("auto choice");
            }

            dialogueOptions_text[s].text = (s+1).ToString() + "] " + dialogueOptions[s].response;
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
    //private void OnDialogueTest()
    //{
    //    DrawNode("What is your name?", true, "Jakub", new List<string> { "Im Jakub", "Go Away", "I LOVE CRACK" });
    //}

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

    //Sends when valid dialogue options chosen. 
    private void SendDialogueChosen(int indexChosen)
    {
        if (!dialogueChosen)
        {
            print("OPTION CHOSE: " + indexChosen.ToString());
            ChosenDialogueTransition(indexChosen - 1);
            FadeOutDialogueOptions();
            GlitchChosenDialogueIntoPosition_Part1(dialogueTextRects[indexChosen - 1]);

            dialogueChosen = true;
            gameCon.PostResponse(indexChosen - 1);
        }
        
    }

    #region RESETTING
    private void ResetReadyToDisplay()
    {
        dialogueChosen = false;
        LeanTween.cancel(dialogueOptions_cg.gameObject);
        LeanTween.cancel(dialogueUI_cg.gameObject);
        ResetTextPositionToOriginal();
    }


    private void SetOriginalTextPositions()
    {
        for(int rt= 0; rt < dialogueTextRects.Count; rt++)
        {
            dialogueTextPositionsStored.Add(dialogueTextRects[rt].anchoredPosition);
        }
        option1_position = new Vector2(dialogueTextRects[0].anchoredPosition.x, dialogueTextRects[0].anchoredPosition.y);
    }
    private void ResetTextPositionToOriginal()
    {
        for (int rt = 0; rt < dialogueTextRects.Count; rt++)
        {
            dialogueTextRects[rt].anchoredPosition = dialogueTextPositionsStored[rt];
        }
    }
    #endregion

    #region UI ANIMATION
    RectTransform optTemp;
    private void GlitchChosenDialogueIntoPosition_Part1(RectTransform opt)
    {
        float additionalOffset = 0;
        if(opt.anchoredPosition.x == 0)
        {
            additionalOffset = 100f;
        }
        LeanTween.move(opt.gameObject, new Vector3(opt.position.x + 200f + additionalOffset, opt.position.y, 0f), 0.00f)
            .setOnComplete(GlitchChosenDialogueIntoPosition_Part2);
        optTemp = opt;
    }
    private void GlitchChosenDialogueIntoPosition_Part2()
    {
        LeanTween.delayedCall(0.07f, GlitchChosenDialogueIntoPosition_Part3);
    }
    private void GlitchChosenDialogueIntoPosition_Part3()
    {
        LeanTween.move(optTemp, new Vector3(option1_position.x, option1_position.y, 0f), 0.00f);
        optTemp = null;
    }
    #endregion


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
        LeanTween.value(dialogueUI_cg.gameObject, UpdateEntireCGAlpha, 1f, 0f, cg_fadout_time).setEase(cg_fadout);
    }
    private void UpdateEntireCGAlpha(float a)
    {
        dialogueUI_cg.alpha = a;
    }

    //fades out dialogue OPTIONS
    private void FadeOutDialogueOptions()
    {
        LeanTween.value(dialogueOptions_cg.gameObject, UpdateOptionsCGAlpha, 1f, 0f, cg_fadout_time).setEase(cg_fadout);
    }
    private void UpdateOptionsCGAlpha(float a)
    {
        dialogueOptions_cg.alpha = a;
    }
    #endregion
}
