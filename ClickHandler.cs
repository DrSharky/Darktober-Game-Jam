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

    void ToggleDot()
    {
        if (redDotInstance == null)
            redDotInstance = Instantiate(redDot, transform);
        else
            Destroy(redDotInstance);

        if (pointPosInstance == null)
        {
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            pointPosInstance = Instantiate(pointPos, screenPoint, new Quaternion());
            pointPosInstance.GetComponent<Text>().text = "(" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")";
            pointPosInstance.transform.parent = GameObject.Find("Canvas").transform;
        }
        //else
        //    Destroy(pointPosInstance);
    }
}