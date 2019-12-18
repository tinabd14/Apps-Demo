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
           // CheckIfItIsAWord();
    }

    private void CheckIfItIsAWord()
    {
        GetSelectedBoxes();
        CheckSelectedBoxes();
    }

    private void CheckSelectedBoxes()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                //if(touch.position)
            }
            else if (touch.phase == TouchPhase.Moved)
            {

            }
            else if (touch.phase == TouchPhase.Ended)
            {

            }
        }
        
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
