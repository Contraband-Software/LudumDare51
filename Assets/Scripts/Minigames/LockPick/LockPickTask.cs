using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickTask : MonoBehaviour
{

    private bool taskDisplaying = false;
    [SerializeField] InteractWithObjects interactObjectsScript;
    [SerializeField] CanvasGroup lockPickTask_cg;

    [Header("SLIDERS")]
    [SerializeField] RectTransform leftSlider;
    [SerializeField] RectTransform leftNotch;


    private void Start()
    {
        interactObjectsScript = GameObject.Find("Player").GetComponent<InteractWithObjects>();
        lockPickTask_cg.alpha = 0f;
    }

    //needs to freeze player first
    public void DisplayLockPick()
    {
        taskDisplaying = true;
        lockPickTask_cg.alpha = 1f;
    }

    private void GetMinMaxSliders()
    {

    }
    
}
