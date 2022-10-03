using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasRoom : MonoBehaviour
{
    [SerializeField] Image blackOut;

    float progress = 0;

    public void StartGassingRoom()
    {
        
    }

    private IEnumerator FadeScreen()
    {
        while (OriginalHeight - characterController.height > CrouchCutOffValue)
        {
            characterController.height += (OriginalHeight - characterController.height) * CrouchSpeed;
            yield return new WaitForSeconds(.001f);
        }
    }
}
