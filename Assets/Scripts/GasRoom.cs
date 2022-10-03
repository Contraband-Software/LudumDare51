using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasRoom : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image blackOut;

    [Header("Settings")]
    [SerializeField] float FadeSpeed = 0.1f;
    [SerializeField, Range(0, 1)] float FadeCutoff = 0.9f;

    float progress = 0;

    public void StartGassingRoom()
    {
        StartCoroutine(FadeScreen());
    }

    private IEnumerator FadeScreen()
    {
        while (progress < FadeCutoff)
        {
            progress += (1 - progress) * FadeSpeed;
            yield return new WaitForSeconds(.001f);
        }

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetDialogueController().PostFlag("GassingDone");
    }
}
