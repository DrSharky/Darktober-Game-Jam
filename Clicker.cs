using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
            EventManager.TriggerEvent("ToggleDots");
	}
}
