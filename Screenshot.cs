using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Screenshot : MonoBehaviour
{
    [SerializeField]
    string modelName;
    [SerializeField]
    GameObject UICanvas;

    string directory = "Screenshots/";
    int lastFileIndex = 1;

    string filePath;

    // Use this for initialization
    void Start ()
    {
        filePath = directory + modelName + lastFileIndex + ".png";

        if (File.Exists(filePath))
        {
            string[] files = Directory.GetFiles(directory);

            string lastFile = files[files.Length - 1].Split('.')[0];
            lastFileIndex = int.Parse(lastFile.Substring(lastFile.Length - 1));
            filePath = directory + modelName + lastFileIndex + ".png";
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
            TakeScreenshot();
    }

    void TakeScreenshot()
    {
        if (File.Exists(filePath))
        {
            string[] files = Directory.GetFiles(directory);

            string lastFile = files[files.Length - 1].Split('.')[0];
            lastFileIndex = int.Parse(lastFile.Substring(lastFile.Length - 1));
            lastFileIndex++;
            filePath = directory + modelName + lastFileIndex + ".png";
        }

        StartCoroutine(NoUIScreenshot());        
    }

    IEnumerator NoUIScreenshot()
    {
        UICanvas.SetActive(false);
        yield return new WaitForEndOfFrame();
        ScreenCapture.CaptureScreenshot(filePath);
        UICanvas.SetActive(true);
    }
}
