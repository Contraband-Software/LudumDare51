using MiniGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStation : MiniGames.UseableObject
{
    public List<string> reqs;

    public override void Use(List<InteractableObject> playInv)
    {
        if (Global.MatchRequirements(playInv, reqs))
        {
            //click sound.play
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetDialogueController().PostFlag("LockPick");
        }
    }
}
