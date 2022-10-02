using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using static Global;

public class ActEnd : MonoBehaviour
{
    [SerializeField] SceneNames scene;

    public void NextAct()
    {
        SceneManager.LoadScene(GetScene(scene));
    }
}
