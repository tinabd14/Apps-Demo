using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public static JSONInventer myJSONInventer;
    
    private List<GameObject> selectedBoxes = new List<GameObject>();
    List<GameObject> allBoxes;

    private void Start()
    {
        myJSONInventer = new JSONInventer();
        allBoxes = Grid.boxes;
    }

    private void Update()
    {
        while(Input.touches.Length > 0)
        {
            CheckIfItIsAWord();
            Debug.Log("TT: " + Input.touches);
        }
    }

    private void CheckIfItIsAWord()
    {
        GetSelectedBoxes();
        CheckSelectedBoxes();
    }

    private void CheckSelectedBoxes()
    {
        Debug.Log(selectedBoxes.Count);
    }

    private void GetSelectedBoxes()
    {
        selectedBoxes.Clear();
        for (int i = 0; i < allBoxes.Count; i++)
        {
            if (allBoxes[i].GetComponent<Box>().GetTouched())
            {
                selectedBoxes.Add(allBoxes[i]);
            }
        }
    }
}
