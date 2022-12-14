using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class LockPickTask : MonoBehaviour
{

    public bool taskDisplaying = false;
    public bool taskComplete = false;
    private int successes = 0;
    InteractWithObjects interactObjectsScript;
    [SerializeField] CanvasGroup lockPickTask_cg;
    private PlayerController pController;

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
    [SerializeField] RectTransform crochetRect;
    [SerializeField] RectTransform paperclipRect;
    private float crochetZRot_og = -140f;
    private float paperclipZRot_og = -240f;
    private float crochetZTarget = -49f;
    private float paperclipZTarget = -190f;
    [SerializeField] GameObject safe;

    [Header("sETTINGS")]
    private bool hasAllComponents = false;
    public float speed1 = 250f;
    public float speed2 = 350f;
    private RectTransform currentSliderNotch;

    [SerializeField] BoxCollider boxController;

    public UnityEvent onTaskSuccess;

    private void Start()
    {
        interactObjectsScript = GameObject.Find("Player").GetComponent<InteractWithObjects>();
        pController = GameObject.Find("Player").GetComponent<PlayerController>();
        lockPickTask_cg.alpha = 0f;

        crochetHookImage.enabled = false;
        paperClipImage.enabled = false;

        currentSliderCoroutine = SliderSlide(leftNotch);
    }

    //needs to freeze player first

    public void DisplayLockPick(List<string> playerInventory)
    {
        pController.Freeze();
        successes = 0;

        taskDisplaying = true;
        lockPickTask_cg.alpha = 1f;

        Vector3 cEuls = crochetRect.eulerAngles;
        cEuls.z = crochetZRot_og;
        crochetRect.eulerAngles = cEuls;

        Vector3 pEuls = paperclipRect.eulerAngles;
        pEuls.z = paperclipZRot_og;
        paperclipRect.eulerAngles = pEuls;


        if (playerInventory.Contains("Crochet Hook"))
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

        if (hasAllComponents)
        {
            StartSliderMotion();
        }
        
    }

    public void HideLockPick()
    {
        pController.UnFreeze();

        taskDisplaying = false;
        lockPickTask_cg.alpha = 0f;

        StopCoroutine(currentSliderCoroutine);
    }

    public void HasAllComponentsForGame()
    {
        hasAllComponents = true;
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
        currentSliderNotch = sliderNotch;

        float target = maxY;
        float speed = 300f;
        if(sliderNotch == leftNotch)
        {
            speed = speed1;
        }
        if (sliderNotch == rightNotch)
        {
            speed = speed2;
        }

        while (true)
        {
            if(target == maxY && sliderNotch.anchoredPosition.y < target)
            {
                sliderNotch.anchoredPosition = new Vector2(sliderNotch.anchoredPosition.x, sliderNotch.anchoredPosition.y + (Time.deltaTime * speed));
            }
            else
            {
                target = minY;
            }
            if(target == minY && sliderNotch.anchoredPosition.y > target)
            {
                sliderNotch.anchoredPosition = new Vector2(sliderNotch.anchoredPosition.x, sliderNotch.anchoredPosition.y - (Time.deltaTime * speed));
            }
            else
            {
                target = maxY;
            }

            yield return null;
        }
    }

    public void LockPickPlace()
    {
        if (hasAllComponents)
        {
            if (currentSliderNotch.anchoredPosition.y < maxY * 0.07f && currentSliderNotch.anchoredPosition.y > minY * 0.07f)
            {
                successes++;
                RotateTool();
                if (successes == 2)
                {
                    print("TASK COMPLETE");
                    StopCoroutine(currentSliderCoroutine);
                    ShutDownTask();
                }
                else
                {
                    StopCoroutine(currentSliderCoroutine);
                    currentSliderCoroutine = SliderSlide(rightNotch);
                    StartCoroutine(currentSliderCoroutine);
                }
            }
            else
            {
                print("FAIL");
                StopCoroutine(currentSliderCoroutine);
                currentSliderCoroutine = SliderSlide(leftNotch);

                successes = 0;

                HideLockPick();
            }
        }

        
    }

    private void RotateTool()
    {
        if(successes == 1)
        {
            Vector3 cEuls = crochetRect.eulerAngles;
            cEuls.z = crochetZTarget;
            crochetRect.eulerAngles = cEuls;
        }
        if(successes == 2)
        {
            Vector3 pEuls = paperclipRect.eulerAngles;
            pEuls.z = paperclipZTarget;
            paperclipRect.eulerAngles = pEuls;
        }
    }


    private void ShutDownTask()
    {
        boxController.enabled = false;
        HideLockPick();
        safe.tag = "Untagged";
        onTaskSuccess.Invoke();
    }
    
}
