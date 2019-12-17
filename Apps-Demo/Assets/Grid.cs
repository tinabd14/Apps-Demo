using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private int rowCount;
    private int colCount;
    [SerializeField] GameObject cellPrefab;
    [SerializeField] GameObject boxPrefab;
    private List<GameObject> cells;
    private  List<Sprite> letterAssets;

    JSONInventer myJSONInventer;

    void Start()
    {
        InitializeJSONInventer();
        InitializeGridProperties();
        FillWithCells();
    }

    private void InitializeGridProperties()
    {
        rowCount = myJSONInventer.GetBoard().boardRow;
        colCount = myJSONInventer.GetBoard().boardCol;
        cells = new List<GameObject>();
        letterAssets = new List<Sprite>();
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

    private void FillWithCells()
    {
        cells.Clear();
        for(int i = 0; i < rowCount; i++)
        {
            for(int j = 0; j < colCount; j++)
            {
                for(int k = 0; k < myJSONInventer.GetBoard().letters.Length; k++)
                {
                    if (i == myJSONInventer.GetBoard().letters[k].rowIndex && j == myJSONInventer.GetBoard().letters[k].colIndex)
                    {
                        GameObject cell = Instantiate(cellPrefab);
                        cell.GetComponent<Cell>().Initialize();
                        cell.GetComponent<Cell>().SetPosition(i, j);
                        /*
                        GameObject box = Instantiate(boxPrefab);
                        box.GetComponent<Box>().SetLetter(GetAssetOfLetter(myJSONInventer.GetBoard().letters[k].letter));
                        box.transform.localScale = cell.transform.localScale;
                        box.transform.parent = cell.transform;
                        */
                        cells.Add(cell);
                    }
                }
            }
        }
    }


    private void GetAllLettersFromAssets()
    {
        letterAssets.Clear();
        var sprites = Resources.LoadAll("alfabe", typeof(Sprite));
        foreach (var sprite in sprites)
            letterAssets.Add(sprite as Sprite);
    }

    private Sprite GetAssetOfLetter(string letter)
    {
        for (int i = 0; i < letterAssets.Count; i++)
        {
            if (letterAssets[i].name.Equals(letter))
                return letterAssets[i];
        }
        return null;
    }
}
