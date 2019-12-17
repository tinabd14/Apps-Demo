using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    float xScale;
    float yScale;
    float xPos;
    float yPos;

    JSONInventer myJSONInventer;
    GameObject grid;

    // Start is called before the first frame update
    public void Initialize()
    {
        InitializeJSONInventer();
        InitializeCellProperties();
    }

    private void InitializeCellProperties()
    {
        grid = GameObject.Find("Grid");
        SetScale(grid);
    }

    public void SetPosition(int row, int col)
    {
        float xStart = (col * xScale) + (-1 * grid.GetComponent<RectTransform>().sizeDelta.x / 2);
        float xFinish = xStart + xScale;
        xPos = (xStart + xFinish) / 2;

        float yStart = (row * yScale) + (-1 * grid.GetComponent<RectTransform>().sizeDelta.y / 2);
        float yFinish = yStart + yScale;
        yPos = (yStart + yFinish) / 2;

        Vector2 pos = new Vector2(xPos, yPos);
        gameObject.GetComponent<RectTransform>().localPosition = pos;
        Debug.Log(gameObject.GetComponent<RectTransform>().localPosition);

    }

    private void SetScale(GameObject grid)
    {
        float boardRow = myJSONInventer.GetBoard().boardRow;
        float boardCol = myJSONInventer.GetBoard().boardCol;

        Vector2 gridScale = new Vector2(grid.GetComponent<RectTransform>().sizeDelta.x, grid.GetComponent<RectTransform>().sizeDelta.y);
        xScale = gridScale.x / boardCol;
        yScale = gridScale.y / boardRow;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(xScale, yScale);
    }

    private void InitializeJSONInventer()
    {
        if (Gameplay.myJSONInventer == null)
        {
            myJSONInventer = new JSONInventer();
        }
        else
        {
            myJSONInventer = Gameplay.myJSONInventer;
        }
    }
}
