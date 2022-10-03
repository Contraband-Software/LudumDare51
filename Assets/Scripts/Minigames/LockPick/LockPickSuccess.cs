using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickSuccess : MonoBehaviour
{
    [SerializeField] Transform safeDoor;

    public void OnLockPickComplete()
    {
        Debug.Log("OPEB DOOR");

        Vector3 doorEuls = safeDoor.eulerAngles;
        doorEuls.z = 94.025f;
        safeDoor.eulerAngles = doorEuls;
    }
}
