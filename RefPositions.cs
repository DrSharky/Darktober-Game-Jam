using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RefPositions
{
    [System.Serializable]
    public struct vectorData
    {
        public Texture2D image;
        public Vector3[] vectorSet;
    }

    public vectorData[] vectorList = new vectorData[20];
}
