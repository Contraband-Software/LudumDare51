

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

        print("I AM FINNA INSPECT THIS BITCH");
        lockPickTask.DisplayLockPick();

        if (Global.MatchRequirements(playInv, inventoryRequirements))
        {
            print("YOU GOT DA SHIT NIGGA");

/*            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetDialogueController().TryPlaySequence("GRDiag");*/
        }
    }
}
