using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private string letter;
    public void ReScaleBox(int boardRow, int boardCol)
    {
        Vector2 scaleVector;
        scaleVector.x = GameObject.Find("Grid").transform.localScale.x / boardRow / boardCol;
        scaleVector.y = GameObject.Find("Grid").transform.localScale.y / boardRow / boardCol;
        gameObject.transform.localScale = scaleVector;
    }

    public void SetLetter(Sprite letterAsset)
    {
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = letterAsset;
    }

  
    public void SetPosition(int rowIndex, int colIndex)
    {

    }

}
