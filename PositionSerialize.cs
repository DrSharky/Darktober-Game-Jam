﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PositionSerialize : MonoBehaviour
{
    [SerializeField]
    GameObject armature;
    string filePath;
    string directory = "Exported Positions/";
    int lastFileIndex = 1;
    string modelName;

    void Start ()
    {
        modelName = armature.transform.parent.gameObject.name;
        filePath = directory + modelName + "/" + lastFileIndex + ".txt";
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.V))
            WritePositions();
        else if (Input.GetKeyDown(KeyCode.G))
            ReadPositions();
	}

    void WritePositions()
    {
        //If file already exists, write to another file & add new count at end of file name.
        if (File.Exists(filePath))
        {
            string[] files = Directory.GetFiles(directory);

            string lastFile = files[files.Length - 1].Split('.')[0];
            lastFileIndex = int.Parse(lastFile.Substring(lastFile.Length - 1));
            lastFileIndex++;
            filePath = directory + modelName + "/" + lastFileIndex + ".txt";
        }

        StreamWriter writer = new StreamWriter(filePath, true);

        for (int i = 0; i < armature.transform.childCount; i++)
        {
            if (!armature.transform.GetChild(i).gameObject.name.Contains("Base"))
                writer.WriteLine(armature.transform.GetChild(i).position);
        }
        writer.Close();
    }

    void ReadPositions()
    {
        StreamReader reader = new StreamReader(filePath);

        //childCount-1 because base bone is excluded.
        for(int i = 0; i < armature.transform.childCount-1; i++)
        {
            Vector3 readPos = new Vector3();
            string[] stringCoordinates = reader.ReadLine().Split('(', ',', ')');
            readPos.x = float.Parse(stringCoordinates[1]);
            readPos.y = float.Parse(stringCoordinates[2]);
            readPos.z = float.Parse(stringCoordinates[3]);

            armature.transform.GetChild(i).position = readPos;

            //DEBUG - SHOW READ POSITIONS
            //Debug.Log(readPos);
        }
        reader.Close();
    }
}