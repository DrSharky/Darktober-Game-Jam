using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Add this script to the armature.
public class Bones : MonoBehaviour
{
    public static List<Vector3> originalPositions;

	void Start ()
    {
        originalPositions = new List<Vector3>();
        SaveOriginals();
	}
	
	void Update ()
    {
		
	}

    void SaveOriginals()
    {
        int boneCount = transform.childCount - 1;
        for (int i = 0; i < boneCount; i++)
        {
            Transform bone = transform.GetChild(i);
            if (!bone.gameObject.name.Contains("Base"))
                originalPositions.Add(bone.position);
        }
    }
}
