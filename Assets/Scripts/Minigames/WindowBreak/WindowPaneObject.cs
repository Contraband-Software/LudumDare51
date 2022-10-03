using MiniGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowPaneObject : MiniGames.UseableObject
{
    [SerializeField] List<string> reqs;

    bool breakable = false;

    public void SetBreakable()
    {
        breakable = true;

        foreach (Transform child in transform)
        {
            child.gameObject.tag = "Useable";
        }
    }

    public override void Use(List<InteractableObject> playInv)
    {
        if (Global.MatchRequirements(playInv, reqs) && breakable)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetDialogueController().PostFlag("WindowBroken");
        }
    }
}
