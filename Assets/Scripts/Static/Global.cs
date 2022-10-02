using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public enum SceneNames
    {
        MAINMENU,
        ACT1,
        ACT2
    }

    public static string GetScene(SceneNames scene)
    {
        switch (scene)
        {
            case SceneNames.MAINMENU:
                return "MainMenu";
                break;
            case SceneNames.ACT1:
                return "SamPrototyping";
                break;
            case SceneNames.ACT2:
                return "SamPrototyping2";
                break;
        }

        throw new System.Exception("WRONG SCENE NAME");
    }
}
