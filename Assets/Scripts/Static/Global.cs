using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public enum SceneNames
    {
        MAINMENU,
        TUTORIAL,
        ACT1,
        ACT2
    }

    public static bool MatchRequirements(List<MiniGames.InteractableObject> playerInv, List<string> inventoryRequirements)
    {
        List<string> playerInv_string = new List<string>();
        foreach (MiniGames.InteractableObject obj in playerInv)
        {
            playerInv_string.Add(obj.GetObjectName());
        }
        foreach (string req in inventoryRequirements)
        {
            if (!playerInv_string.Contains(req))
            {
                return false;
            }
        }

        return true;
    }

    public static string GetScene(SceneNames scene)
    {
        switch (scene)
        {
            case SceneNames.MAINMENU:
                return "MainMenu";
                break;
            case SceneNames.TUTORIAL:
                return "Tutorial";
                break;
            case SceneNames.ACT1:
                return "Act1";
                break;
        }

        throw new System.Exception("WRONG SCENE NAME");
    }
}
