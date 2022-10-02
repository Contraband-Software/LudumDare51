using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames
{
    public abstract class UseableObject : AbstractBaseObject
    {
        private void Awake()
        {
            gameObject.tag = "Useable";
        }

        public abstract void Use(List<InteractableObject> playInv);
    }
}