

using MiniGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeObject : MiniGames.UseableObject
{
    [SerializeField] List<string> inventoryRequirements = new List<string>();
    [SerializeField] LockPickTask lockPickTask;

    public override void Use(List<InteractableObject> playInv)
    {
        //show interface
        //hand off game logic to another gameobject (child)

        List<string> playerInvString = new List<string>();
        foreach(InteractableObject obj in playInv)
        {
            playerInvString.Add(obj.GetObjectName());
        }

        if (Global.MatchRequirements(playInv, inventoryRequirements))
        {
            lockPickTask.HasAllComponentsForGame();
            lockPickTask.DisplayLockPick(playerInvString);
            /*            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetDialogueController().TryPlaySequence("GRDiag");*/
        }
        else
        {
            lockPickTask.DisplayLockPick(playerInvString);
        }
    }
}
