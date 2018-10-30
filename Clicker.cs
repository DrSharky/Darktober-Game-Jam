using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clicker : MonoBehaviour
{
    private bool toggleOn = false;
    private bool controlsEnabled = false;
    private Action disableControlsListener;
    private Action enableControlsListener;

    void Start()
    {
        enableControlsListener = new Action(() => { controlsEnabled = true; });
        disableControlsListener = new Action(() => { controlsEnabled = false; });
        EventManager.StartListening("EnableControls", enableControlsListener);
        EventManager.StartListening("DisableControls", disableControlsListener);
    }
    

	void Update ()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) && !toggleOn && controlsEnabled)
        {
            EventManager.TriggerEvent("ToggleDots");
        }

        if (Input.GetMouseButtonDown(1) && controlsEnabled)
        {
            EventManager.TriggerEvent("ToggleDots");
            toggleOn = !toggleOn;
        }
	}
}
