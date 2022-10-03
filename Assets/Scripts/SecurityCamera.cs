using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public GameObject target;
    public bool targetDetected = false;
    public float detectionTresh;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = target.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraToTarget = Vector3.Normalize(target.transform.position - this.transform.position);
        if(Vector3.Dot(cameraToTarget,this.transform.right)> detectionTresh)
        {
            RaycastHit hit;
            if(!Physics.Raycast(transform.position,cameraToTarget,out hit) || hit.transform.gameObject==target)
            {
                targetDetected = true;
            }
            else
            {
                targetDetected = false;
            }
        }
        else
        {
            targetDetected = false;
        }
        //Debug.Log(targetDetected);
    }
}
