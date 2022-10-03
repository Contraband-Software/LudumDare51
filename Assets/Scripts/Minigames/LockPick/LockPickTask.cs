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
    [SerializeField] RectTransform rightSlider;
    [SerializeField] RectTransform rightNotch;
    private float maxY;
    private float minY;

    IEnumerator currentSliderCoroutine;

    [Header("OBJECTS IN LOCK")]
    [SerializeField] Image crochetHookImage;
    [SerializeField] Image paperClipImage;

    [Header("sETTINGS")]
    public float speed1 = 250f;
    public float speed2 = 350f;

    private void Start()
    {
        interactObjectsScript = GameObject.Find("Player").GetComponent<InteractWithObjects>();
        lockPickTask_cg.alpha = 0f;

        crochetHookImage.enabled = false;
        paperClipImage.enabled = false;

        currentSliderCoroutine = SliderSlide(leftNotch);
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

        StartSliderMotion();
    }

    public void HideLockPick()
    {
        taskDisplaying = false;
        lockPickTask_cg.alpha = 0f;

        StopCoroutine(currentSliderCoroutine);
    }


    private void GetMinMaxSliders()
    {
        maxY = leftSlider.sizeDelta.y/2 * 0.9f;
        minY = maxY * -1f;
    }

    private void StartSliderMotion()
    {
        StartCoroutine(currentSliderCoroutine);
    }

    private IEnumerator SliderSlide(RectTransform sliderNotch)
    {
        float target = maxY;
        if(sliderNotch == leftNotch)
        {

        }
        if (sliderNotch == rightNotch)
        {

        }
        print("starting sliding");

        while (true)
        {
            print("..");
            if(target == maxY && sliderNotch.anchoredPosition.y < target)
            {
                sliderNotch.anchoredPosition = new Vector2(sliderNotch.anchoredPosition.x, sliderNotch.anchoredPosition.y + (Time.deltaTime * speed1));
            }
            else
            {
                target = minY;
            }
            if(target == minY && sliderNotch.anchoredPosition.y > target)
            {
                sliderNotch.anchoredPosition = new Vector2(sliderNotch.anchoredPosition.x, sliderNotch.anchoredPosition.y - (Time.deltaTime * speed2));
            }
            else
            {
                target = maxY;
            }

            yield return null;
        }
    }
    
}
