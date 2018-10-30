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

    private bool disableControl = false;

    Action toggleDotListener;
    Action finishListener;

    void Start()
    {
        toggleDotListener = new Action(ToggleDot);
        finishListener = new Action(DeleteDots);
        EventManager.StartListening("ToggleDots", toggleDotListener);
        EventManager.StartListening("Finish", finishListener);
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        if (!disableControl)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }

    void ToggleDot()
    {
        if (redDotInstance == null)
            redDotInstance = Instantiate(redDot, transform);
        else
            Destroy(redDotInstance);
    }

    void DeleteDots()
    {
        disableControl = true;
        GameObject[] dots = GameObject.FindGameObjectsWithTag("Reddot");
        for(int i = 0; i < dots.Length; i++)
        {
            Destroy(dots[i]);
        }
    }
}