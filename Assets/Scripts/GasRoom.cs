using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class GasRoom : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image blackOut;
    [SerializeField] AudioSource hissing;

    [Header("Settings")]
    [SerializeField, Min(0)] float FadeSpeed = 0.1f;
    [SerializeField, Range(0, 1)] float FadeCutoff = 0.9f;

    float progress = 0;

    private void Awake()
    {
        GetComponent<GraphSystem.ActionComponent>().action = () => { StartGassingRoom(); };
    }

    public void StartGassingRoom()
    {
        hissing.Play();
        StartCoroutine(FadeScreen());
    }

    private IEnumerator FadeScreen()
    {
        while (progress < FadeCutoff)
        {
            progress += (1 - progress) * FadeSpeed;

            Color newColor = blackOut.color;
            newColor.a = progress;
            blackOut.color = newColor;

            yield return new WaitForSeconds(.001f);
        }

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetDialogueController().PostFlag("GassingDone");
    }
}
