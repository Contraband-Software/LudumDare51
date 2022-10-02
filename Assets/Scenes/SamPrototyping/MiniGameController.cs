using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameController : MonoBehaviour
{
    private void Start()
    {
        GetComponent<GraphSystem.ActionComponent>().action = Meth;
    }

    public void Meth()
    {
        Debug.Log("meth");
    }
}
