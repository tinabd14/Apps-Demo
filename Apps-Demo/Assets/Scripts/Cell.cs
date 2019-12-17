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
    int rowIndex;
    int colIndex;

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
        float xStart = (col * xScale) + (-1 * grid.transform.localScale.x / 2) + grid.transform.position.x;
        float xFinish = xStart + xScale;
        xPos = (xStart + xFinish) / 2;

        float yStart = (row * yScale) + (-1 * grid.transform.localScale.y / 2) + grid.transform.position.y;
        float yFinish = yStart + yScale;
        yPos = (yStart + yFinish) / 2;

        Vector2 pos = new Vector2(xPos, yPos);
        gameObject.transform.position = pos;
    }

    private void SetScale(GameObject grid)
    {
        float boardRow = myJSONInventer.GetBoard().boardRow;
        float boardCol = myJSONInventer.GetBoard().boardCol;

        Vector2 gridScale = grid.transform.localScale;
        xScale = gridScale.x / boardCol;
        yScale = gridScale.y / boardRow;
        
        gameObject.transform.localScale = new Vector2(xScale, yScale);
    }

    private void InitializeJSONInventer()
    {
        if (Gameplay.myJSONinventer == null)
        {
            myJSONInventer = new JSONInventer();
        }
        else
        {
            myJSONInventer = Gameplay.myJSONinventer;
        }
    }
}
