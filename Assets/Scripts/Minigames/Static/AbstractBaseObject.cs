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
            return ObjectName;
        }

        public UnityEvent OnInteract;
    }
}