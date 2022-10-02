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
    [SerializeField] RectTransform speakerCanvasRect;
    [SerializeField] RectTransform timeOutCanvasRect;
    [SerializeField] RectTransform timeOutBar;
    [SerializeField] TextMeshProUGUI currentDialogue_text;
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
    private float originalSpeakerYpos;

    //Dialogue Variables
    private List<GraphConnections.ResponseConnectionData> current_DialogueOptions = new List<GraphConnections.ResponseConnectionData>();
    private int diagOptions = 0;
    private string current_SpeakerName;
    private bool canReplyCurrent = false;
    int autoChooseChoice = 0;

    //COROUTINES
    private IEnumerator noOptionTimeOutReply;
    private IEnumerator countdownDisplay;
    private IEnumerator delayedResponsePostage;

    //Game Controls
    DialogueSequenceController dialogCon;

    private void Start()
    {
        dialogCon = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetDialogueController();
        SetOriginalTextPositions();

        noOptionTimeOutReply = NoOptionTimeOutReply(0,0);
        countdownDisplay = CountdownDisplay(0);
        delayedResponsePostage = DelayedResponsePostage(0, 0);
    }

#region GAME CONTROLLER INTERFACE
    public void OnWait(float time, bool clear)
    {
        Debug.Log(time);
        Debug.Log(clear);
        if (clear)
        {
            Debug.Log("CLEARING");
            dialogueUI_cg.alpha = 0f;
        }
        
    }

    //Pass in the speaker name and list of options
    //shows UI for the given data. Should be CALLED ONCE per dialogue change
    public void DrawNode(string incomingText, bool canReply, string speakerName, List<GraphConnections.ResponseConnectionData> dialogueOptions, float timeOut, AudioClip clip)
    {
        Debug.Log("DrawNode: " + incomingText);

        //COROTUINE RESET
        StopCoroutine(noOptionTimeOutReply);
        StopCoroutine(countdownDisplay);

        MoveAllTransitionedBack();
        InstantSoftClearDialogueOptions();
        InstantDialoguePanelOpaque();
        ResetReadyToDisplay();

        current_SpeakerName = speakerName;
        current_DialogueOptions = dialogueOptions;
        diagOptions = dialogueOptions.Count;
        currentDialogue_text.text = incomingText;

        canReplyCurrent = canReply;
        autoChooseChoice = 0;

        //UPDATE TEXTS TO REFLECT RESPONSE PROMPT
        speakerName_text.text = speakerName + ": ";
        float smallestTextSize = Mathf.Infinity;

        if (canReply)
        {
            print("CAN REPLY");
            print("OPTIONS: " + dialogueOptions.Count);
            for (int s = 0; s < dialogueOptions.Count; s++)
            {
                if (dialogueOptions[s].AutoChoose)
                {
                    autoChooseChoice = s;
                    //Debug.Log("auto choice");
                }

                dialogueOptions_text[s].text = (s + 1).ToString() + "] " + dialogueOptions[s].response;
                if (dialogueOptions_text[s].fontSize < smallestTextSize)
                {
                    smallestTextSize = dialogueOptions_text[s].fontSize;
                }
            }

            //UNIFORMIZE DIALOGUE OPTIONS FONT SIZES
            foreach (TextMeshProUGUI t in dialogueOptions_text)
            {
                t.enableAutoSizing = false;
                t.fontSize = smallestTextSize;
            }

            countdownDisplay = CountdownDisplay(timeOut + clip.length);
            StartCoroutine(countdownDisplay);
        }
        else
        {
            speakerCanvasRect.anchoredPosition = new Vector2(speakerCanvasRect.anchoredPosition.x, 0f + timeOutCanvasRect.sizeDelta.y);

            countdownDisplay = CountdownDisplay(timeOut + clip.length);
            noOptionTimeOutReply = NoOptionTimeOutReply(timeOut + clip.length, 0);
            StartCoroutine(noOptionTimeOutReply);
            StartCoroutine(countdownDisplay);
        }
    }

    IEnumerator NoOptionTimeOutReply(float time, int index)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("NoOptionTimeOutReply + WaitForSeconds: " + time.ToString());

        StopCoroutine(countdownDisplay);
        SendDialogueBlank(index);

        yield break;
    }
    #endregion


    #region TIMING
    private IEnumerator CountdownDisplay(float startingTime)
    {
        Debug.Log("COUNTDOWN STARTING TIME: " + startingTime.ToString());
        timeOutBar.localScale = new Vector2(1f, 1f);
        float fullTime = startingTime;
        float remainingTime = fullTime;
        while(remainingTime > 0)
        {
            //Debug.Log("REMAINING TIME: " + remainingTime.ToString());
            remainingTime -= Time.deltaTime;
            timeOutBar.localScale = new Vector2((remainingTime/ fullTime), timeOutBar.localScale.y);
            yield return null;
        }
        if (canReplyCurrent)
        {
            SendDialogueChosen(autoChooseChoice+1);
        }
    }
    #endregion

    public void SelectDialogueOption(int option)
    {
        if(option <= diagOptions)
        {
            SendDialogueChosen(option);
        }
        else
        {
            throw new System.Exception("ERROR - UI CONTROLLER INVALID OPTION");
        }
    }

    //Sends when valid dialogue options chosen. 
    private void SendDialogueChosen(int indexChosen)
    {
        if (!dialogueChosen && canReplyCurrent)
        {
            Debug.Log(dialogueChosen);
            print("OPTION CHOSE: " + indexChosen.ToString());
            dialogueOptions_cg.alpha = 0f;
            speakerCanvasRect.anchoredPosition = new Vector2(speakerCanvasRect.anchoredPosition.x, 0f);
            speakerName_text.text = "YOU:";
            currentDialogue_text.text = current_DialogueOptions[indexChosen - 1].response;

            dialogueChosen = true;
            print(timeOutBar.localScale.x.ToString());
            timeOutBar.localScale = new Vector2(0f, 1f);
            print(timeOutBar.localScale.x.ToString());

            //Get and play correct audio clip from List<GraphConnections.ResponseConnectionData> dialogueOptions
            //apply correct clip length delay

            StopCoroutine(noOptionTimeOutReply);
            StopCoroutine(countdownDisplay);

            delayedResponsePostage = DelayedResponsePostage(dialogCon.GetGlobalTimeDelay(), indexChosen);
            StartCoroutine(delayedResponsePostage);
        }
        
    }

    private IEnumerator DelayedResponsePostage(float time, int indexChosen)
    {
        yield return new WaitForSeconds(time);
        dialogCon.PostResponse(indexChosen - 1);
    }

    private void SendDialogueBlank(int indexChosen)
    {
        dialogueChosen = true;
        dialogCon.PostResponse(indexChosen - 1);
    }

    #region RESETTING
    private void ResetReadyToDisplay()
    {
        Debug.Log("ResetReadyToDisplay");
        dialogueChosen = false;
        //LeanTween.cancel(dialogueOptions_cg.gameObject);
        //LeanTween.cancel(dialogueUI_cg.gameObject);
        ResetTextPositionToOriginal();
    }


    private void SetOriginalTextPositions()
    {
        originalSpeakerYpos = speakerCanvasRect.anchoredPosition.y;

        for(int rt= 0; rt < dialogueTextRects.Count; rt++)
        {
            dialogueTextPositionsStored.Add(dialogueTextRects[rt].anchoredPosition);
        }
        option1_position = new Vector2(dialogueTextRects[0].anchoredPosition.x, dialogueTextRects[0].anchoredPosition.y);
    }
    private void ResetTextPositionToOriginal()
    {
        speakerCanvasRect.anchoredPosition = new Vector2(speakerCanvasRect.anchoredPosition.x, originalSpeakerYpos);

        for (int rt = 0; rt < dialogueTextRects.Count; rt++)
        {
            dialogueTextRects[rt].anchoredPosition = dialogueTextPositionsStored[rt];
        }
    }
    #endregion

    #region UI ANIMATION
    private void GlitchChosenDialogueIntoPosition_Part1(RectTransform opt)
    {
        //print("Glitch part 1");

        float additionalOffset = 0;
        if(opt.anchoredPosition.x == 0)
        {
            additionalOffset = 100f;
        }

        opt.anchoredPosition = new Vector3(opt.anchoredPosition.x + 200f + additionalOffset, opt.anchoredPosition.y, 0f);
        StartCoroutine(GlitchChosenDialogueIntoPosition_Part2(opt));
    }
    private IEnumerator GlitchChosenDialogueIntoPosition_Part2(RectTransform opt)
    {
        //print("Glitch part 2");
        yield return new WaitForSeconds(0.1f);
        opt.anchoredPosition = new Vector2(option1_position.x, option1_position.y);
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

    #endregion
}
