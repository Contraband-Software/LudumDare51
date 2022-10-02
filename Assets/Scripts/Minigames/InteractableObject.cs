using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames {
    public class InteractableObject : AbstractBaseObject
    {
        private void Awake()
        {
            gameObject.tag = "Interactable";
        }
    }
}