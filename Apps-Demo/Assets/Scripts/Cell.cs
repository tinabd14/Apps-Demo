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
    Vector2 rowcol;


    JSONInventer myJSONInventer;
    GameObject grid;
    [SerializeField] GameObject childBox;
    [SerializeField] List<GameObject> neighbours = new List<GameObject>();


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
        rowIndex = row;
        colIndex = col;
        rowcol = new Vector2(rowIndex, colIndex);

        float xStart = (col * xScale) + (-1 * grid.GetComponent<RectTransform>().sizeDelta.x / 2);
        float xFinish = xStart + xScale;
        xPos = (xStart + xFinish) / 2;

        float yStart = (row * yScale) + (-1 * grid.GetComponent<RectTransform>().sizeDelta.y / 2);
        float yFinish = yStart + yScale;
        yPos = (yStart + yFinish) / 2;

        Vector2 pos = new Vector2(xPos, yPos);
        gameObject.GetComponent<RectTransform>().localPosition = pos;
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

    public Vector2 GetRowCol()
    {
        return rowcol;
    }

    public int GetRowIndex()
    {
        return rowIndex;
    }
    public int GetColIndex()
    {
        return colIndex;
    }

    public List<GameObject> GetNeighbourCells()
    {
        return neighbours;
    }

    public void SetNeighbourCells(List<GameObject> neighbours)
    {
        this.neighbours.Clear();
        this.neighbours = neighbours;
    }

    public GameObject GetChildBox()
    {
        return childBox;
    }


    public void SetChildBox(GameObject child)
    {
        childBox = child;
    }
}
