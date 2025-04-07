using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsProvider : MonoBehaviour
{
    private Transform buttonContainer;
    private List<Button> buttons;
    
    private void Awake()
    {
        buttons = new List<Button>();
        buttonContainer = this.transform.GetChild(0).GetChild(0);

        foreach (Transform button in buttonContainer)
        {
            buttons.Add(button.GetComponent<Button>());
        }
    }

    public void SetNewButtons()
    {
        CubeManagement.Instance.buttons = buttons;
    }

}
