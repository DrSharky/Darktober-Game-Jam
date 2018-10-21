using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    bool toggleOn = false;

	void Update ()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) && !toggleOn)
            EventManager.TriggerEvent("ToggleDots");

        if (Input.GetKeyDown(KeyCode.F))
        {
            EventManager.TriggerEvent("ToggleDots");
            toggleOn = !toggleOn;
        }
	}
}
