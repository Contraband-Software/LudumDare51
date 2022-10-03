using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MiniGames
{
    public abstract class AbstractBaseObject : MonoBehaviour
    {
        [SerializeField] string ObjectName;

        public string GetObjectName() {
#if UNITY_EDITOR
            if (ObjectName == null)
            {
                throw new System.Exception("AbstractBaseObject: INTERACTABLE OBJECT NAME NOT SET");
            }
#endif
            return ObjectName;
        }

        public UnityEvent OnInteract;
    }
}