using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PositionComparison : MonoBehaviour
{
    [SerializeField]
    private GameObject armature;

    private const float MAX_POINTS = 100.0f;
    private int boneCount = 0;
    private float pointTotal = 0.0f;
    private int refImageIndex;

    [SerializeField]
    private RefPositions refPositions;

    private List<Vector3> currentPositions;
    private Vector3[] selectedRef;

    private Action calculateScoreListener;

    public static double scoreFinal = 0.0;

    public static Texture2D refImage;

	// Use this for initialization
	void Start ()
    {
        //Use -1 to exclude the base bone.
        boneCount = armature.transform.childCount - 1;
        refImageIndex = UnityEngine.Random.Range(0,5);
        selectedRef = refPositions.vectorList[refImageIndex].vectorSet;
        refImage = refPositions.vectorList[refImageIndex].image;

        calculateScoreListener = new Action(Compare);
        EventManager.StartListening("Finish", calculateScoreListener);
    }

    void Compare ()
    {
        Debug.Log("Compare");
        double xTotalDiff = 0;
        double yTotalDiff = 0;
        for (int i = 0; i < boneCount; i++)
        {
            Vector3 refVector = selectedRef[i];
            double xFinal = Math.Round(Math.Abs(armature.transform.GetChild(i).position.x), 1);
            double yFinal = Math.Round(Math.Abs(armature.transform.GetChild(i).position.y), 1);

            double xRef = Mathf.Abs(selectedRef[i].x);
            double yRef = Mathf.Abs(selectedRef[i].y);

            xTotalDiff += Math.Abs(xFinal - xRef);
            yTotalDiff += Math.Abs(yFinal - yRef);
        }

        //Grade factor is 0.125, received from formula below. works
        //for pumpkin, not sure about other models...
        Debug.Log("X Total Diff: " + xTotalDiff);
        Debug.Log("Y Total Diff: " + yTotalDiff);
        double boneFloat = boneCount;
        if (xTotalDiff > 10)
            xTotalDiff -= 10;
        if (yTotalDiff > 10)
            yTotalDiff -= 10;
        double combinedDiff = (xTotalDiff + yTotalDiff) * (1 / (boneFloat / (100 / (boneFloat * 2))));
        Debug.Log("Combined Diff: " + combinedDiff);
        scoreFinal = Math.Round((MAX_POINTS - combinedDiff), 1);
        if (scoreFinal < 0)
            scoreFinal = 0;
        Debug.Log("Score: " + scoreFinal);
    }
}