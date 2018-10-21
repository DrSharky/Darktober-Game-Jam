using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour
{
    Cursor pointer;
    Vector3 screenPoint;
    Vector3 offset;
    [SerializeField]
    GameObject redDot;
    GameObject redDotInstance;
    [SerializeField]
    GameObject pointPos;
    GameObject pointPosInstance;

    Action toggleDotListener;

    void Start()
    {
        toggleDotListener = new Action(ToggleDot);
        EventManager.StartListening("ToggleDots", toggleDotListener);
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        EventManager.TriggerEvent("ToggleDots");
    //    }
    //}

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    // -- CREATED FOR DEBUG PURPOSES!!! -- DELETE LATER --
    //private void OnMouseUp()
    //{
    //    Vector3 screenPt = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    //    Debug.Log("Screen Pos: " + screenPt);
    //    Debug.Log("World Pos: " + transform.position);
    //}
    // -- DEBUG ONLY!! -- DELETE AFTER USE --

    void ToggleDot()
    {
        if (redDotInstance == null)
            redDotInstance = Instantiate(redDot, transform);
        else
            Destroy(redDotInstance);

        //TogglePositionDisplay();
    }

    // -- CREATED FOR DEBUG PURPOSES!!! -- DELETE LATER --
    void TogglePositionDisplay()
    {
        Vector3 screenPosition;
         if (pointPosInstance == null)
         {
             screenPosition = Camera.main.WorldToScreenPoint(transform.position);
             pointPosInstance = Instantiate(pointPos, screenPosition, new Quaternion());
             pointPosInstance.GetComponent<Text>().text = "(" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")";
             pointPosInstance.transform.parent = GameObject.Find("Canvas").transform;
         }
         else
            Destroy(pointPosInstance);
    }
    // -- DEBUG ONLY!! -- DELETE AFTER USE --
}