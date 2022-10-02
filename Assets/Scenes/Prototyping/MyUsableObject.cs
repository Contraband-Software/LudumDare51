using MiniGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUsableObject : MiniGames.UseableObject
{
    [SerializeField] List<string> inventoryRequirements = new List<string>();

    public override void Use(List<InteractableObject> playerInv)
    {
        List<string> playerInv_string = new List<string>();
        foreach(InteractableObject obj in playerInv)
        {
            playerInv_string.Add(obj.GetObjectName());
        }
        foreach (string req in inventoryRequirements)
        {
            if(!playerInv_string.Contains(req))
            {
                return;
            }
        }

        //met inventory requirements

        Debug.Log("Start minigame");

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetDialogueController().PostFlag("LockPicked");
    }
}
