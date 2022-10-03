using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockPickTask : MonoBehaviour
{

    public bool taskDisplaying = false;
    [SerializeField] InteractWithObjects interactObjectsScript;
    [SerializeField] CanvasGroup lockPickTask_cg;

    [Header("SLIDERS")]
    [SerializeField] RectTransform leftSlider;
    [SerializeField] RectTransform leftNotch;
    private float maxY;
    private float minY;

    [Header("ADDITIONAL UI")]
    [SerializeField] TextMeshProUGUI eToExitText;


    [Header("OBJECTS IN LOCK")]
    [SerializeField] Image crochetHookImage;
    [SerializeField] Image paperClipImage;

    private void Start()
    {
        interactObjectsScript = GameObject.Find("Player").GetComponent<InteractWithObjects>();
        lockPickTask_cg.alpha = 0f;

        crochetHookImage.enabled = false;
        paperClipImage.enabled = false;
    }

    //needs to freeze player first

    public void DisplayLockPick(List<string> playerInventory)
    {
        taskDisplaying = true;
        lockPickTask_cg.alpha = 1f;

        if(playerInventory.Contains("Crochet Hook"))
        {
            crochetHookImage.enabled = true;
        }
        if(playerInventory.Contains("Paper Clip"))
        {
            paperClipImage.enabled = true;
        }

        interactObjectsScript.lockPickTask = this;
        interactObjectsScript.NextInteractIsExit();
        interactObjectsScript.HideInteractionPrompt();

        GetMinMaxSliders();
    }

    public void HideLockPick()
    {
        taskDisplaying = false;
        lockPickTask_cg.alpha = 0f;
    }


    private void GetMinMaxSliders()
    {
        maxY = leftSlider.sizeDelta.y/2 * 0.9f;
        minY = leftSlider.sizeDelta.y/2 * 0.1f;

        leftNotch.anchoredPosition = new Vector2(leftNotch.anchoredPosition.x, maxY);
    }
    
}
