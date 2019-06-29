using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    public void PrevButtonClick()
    {
        SceneControlManager.Instance.PrevScene();
    }

    public void NextButtonClick()
    {
        SceneControlManager.Instance.NextScene();
    }
}
