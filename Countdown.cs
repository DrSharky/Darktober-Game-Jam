using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Countdown : MonoBehaviour
{
    [SerializeField]
    private RawImage refImage;
    [SerializeField]
    private GameObject finishText;

    [SerializeField]
    private GameObject tryAgainButton;
    [SerializeField]
    private GameObject quitButton;

    private Vector2 refImagePos;
    private Vector2 cornerPos;
    [SerializeField]
    private GameObject countdownText;


    private bool movedToCorner = false;
    private bool cornerWaitTime = false;
    private bool doneMoving = false;
    private bool counting = false;
    private float countdownTime = 45.0f;

    private Action startTimerListener;

	// Use this for initialization
	void Start()
	{
        cornerPos = new Vector2(-152.0f, -100.0f);
        startTimerListener = new Action(() => { StartCoroutine(CountStart()); });
        EventManager.StartListening("StartTimer", startTimerListener);
        StartCoroutine(ShowRefImage());
	}

    IEnumerator ShowRefImage()
    {
        yield return new WaitForSeconds(0.25f);
        refImage.texture = PositionComparison.refImage;
        refImage.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update()
	{
        if (!movedToCorner)
        {
            StartCoroutine(MoveToCorner());
            movedToCorner = true;
        }

        if (cornerWaitTime)
        {
            refImage.rectTransform.anchorMax = new Vector2(1.0f, 1.0f);
            refImage.rectTransform.anchorMin = new Vector2(1.0f, 1.0f);
            refImage.rectTransform.anchoredPosition = cornerPos;
            EventManager.TriggerEvent("StartTimer");
            EventManager.TriggerEvent("ToggleControls");
        }
        if (counting)
        {
            countdownTime -= Time.deltaTime;
            countdownText.GetComponent<Text>().text = Mathf.FloorToInt(countdownTime).ToString();
        }


        if(countdownTime < 0.2 && countdownTime != -1)
        {
            EventManager.TriggerEvent("DisableControls");
            countdownTime = -1;
            counting = false;
            EventManager.TriggerEvent("Finish");
            countdownText.SetActive(false);
            StartCoroutine(Finish());
            return;
        }

	}

    IEnumerator Finish()
    {
        finishText.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        Text finishTextComp = finishText.GetComponent<Text>();
        if (PositionComparison.scoreFinal < 35)
            finishTextComp.color = new Color(255, 0, 0);
        else if (PositionComparison.scoreFinal > 35 && PositionComparison.scoreFinal < 75)
        {
            finishTextComp.color = new Color(255.0f, 255.0f, 0);
        }
        else
        {
            finishTextComp.color = new Color(0, 255, 0);
        }
        finishText.GetComponent<Text>().text = "Your Score: " + PositionComparison.scoreFinal.ToString();

        yield return new WaitForSeconds(4.0f);
        quitButton.SetActive(true);
        tryAgainButton.SetActive(true);

    }

    IEnumerator MoveToCorner()
    {
        yield return new WaitForSeconds(3.0f);
        cornerWaitTime = true;
    }

    IEnumerator CountStart()
    {
        countdownText.GetComponent<Text>().text = "GO!";
        EventManager.TriggerEvent("EnableControls");
        yield return new WaitForSeconds(1.0f);
        counting = true;
    }
}