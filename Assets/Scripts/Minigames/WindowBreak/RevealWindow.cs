using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealWindow : MonoBehaviour
{
    public GameObject windowCurtains;

    void Start()
    {
        GetComponent<GraphSystem.ActionComponent>().action = () =>
        {
            windowCurtains.SetActive(false);
        };
    }
}
