using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenue : MonoBehaviour
{
    public Camera[] cameras;
    public Material vhs;
    Camera activeCam;
    public Canvas canvas;
    int activeCamIndex = 0;
    float timeToSwitch = 4;
    // Start is called before the first frame update
    void Start()
    {
        activeCam = cameras[0];
    }

    // Update is called once per frame
    void Update()
    {
        canvas.worldCamera = activeCam;
        for(int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == activeCam)
            {
                cameras[i].enabled = true;
            }
            else
            {
                cameras[i].enabled = false;
            }
        }
        if(timeToSwitch < 0)
        {
            timeToSwitch = 4;
            activeCamIndex = (activeCamIndex + 1) % cameras.Length;
            activeCam = cameras[activeCamIndex];
            vhs.SetFloat("_EdgeFuzz", 0.01f);
        }
        timeToSwitch -= Time.deltaTime;
        if (vhs.GetFloat("_EdgeFuzz") > 0.0008f)
        {
            Debug.Log(vhs.GetFloat("_EdgeFuzz"));
            vhs.SetFloat("_EdgeFuzz", vhs.GetFloat("_EdgeFuzz")-0.00003f);
        }
    }

    public void startClicked()
    {
        SceneManager.LoadScene("tutorial");
        
    }
    public void exitClicked()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
