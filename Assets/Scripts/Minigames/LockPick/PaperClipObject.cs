

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperClipObject : MonoBehaviour
{
    [SerializeField] GameObject note;

    public void OnPickupClip()
    {
        Destroy(note);
    }
}
