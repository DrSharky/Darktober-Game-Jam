using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PositionComparison : MonoBehaviour
{
    [SerializeField]
    GameObject armature;

    const float MAX_POINTS = 100.0f;
    int boneCount = 0;
    float pointTotal = 0.0f;

    [SerializeField]
    RefPositions refPositions;

    List<Vector3> currentPositions;
    Vector3[] selectedRef;

	// Use this for initialization
	void Start ()
    {
        //Use -1 to exclude the base bone.
        boneCount = armature.transform.childCount - 1;
        selectedRef = refPositions.vectorList[UnityEngine.Random.Range(0,1)].vectorSet;
        Debug.Log(selectedRef);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Compare();
	}

    void Compare ()
    {
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
        double combinedDiff = (xTotalDiff + yTotalDiff) * (1 / (boneFloat / (100 / (boneFloat * 2))));
        Debug.Log("Combined Diff: " + combinedDiff);
        double scoreFinal = Math.Round((MAX_POINTS - combinedDiff), 1);
        if (scoreFinal < 0)
            scoreFinal = 0;
        Debug.Log("Score: " + scoreFinal);
    }
}