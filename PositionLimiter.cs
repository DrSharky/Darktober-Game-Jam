using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLimiter : MonoBehaviour
{
    GameObject armature;
    int boneCount;
    [SerializeField]
    int distanceLimit = 20;

    void Start()
    {
        armature = transform.parent.gameObject;
        boneCount = armature.transform.childCount-1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            ResetDefaults();
    }

    void OnMouseUp()
    {
        CheckPositionLimits();
    }

    //Function to check that the x & y coordinates of each bone
    //don't go past a limit of +/- (distanceLimit) units from the original position.
    void CheckPositionLimits()
    {
        int boneIndex = transform.GetSiblingIndex();
        float xBone = Mathf.Abs(transform.position.x);
        float xOriginal = Mathf.Abs(Bones.originalPositions[boneIndex].x);
        float yBone = Mathf.Abs(transform.position.y);
        float yOriginal = Mathf.Abs(Bones.originalPositions[boneIndex].y);

        float xDiff = Mathf.Abs(xBone - xOriginal);
        float yDiff = Mathf.Abs(yBone - yOriginal);

        if (xDiff > 20)
        {
            if (transform.position.x < Bones.originalPositions[boneIndex].x)
                transform.position = new Vector3(Bones.originalPositions[boneIndex].x - distanceLimit,
                    transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(Bones.originalPositions[boneIndex].x + distanceLimit,
                    transform.position.y, transform.position.z);
        }

        if (yDiff > 20)
        {
            if (transform.position.y < Bones.originalPositions[boneIndex].y)
                transform.position = new Vector3(transform.position.x,
                    Bones.originalPositions[boneIndex].y - distanceLimit, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x,
                    Bones.originalPositions[boneIndex].y + distanceLimit, transform.position.z);
        }
    }

    void ResetDefaults()
    {
        for (int i = 0; i < Bones.originalPositions.Count; i++)
        {
            armature.transform.GetChild(i).position = Bones.originalPositions[i];
        }
    }
}
