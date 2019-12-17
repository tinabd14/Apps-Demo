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
        GetAllLettersFromAssets();
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
                        
                        GameObject box = Instantiate(boxPrefab, cell.transform);

                        box.GetComponent<Box>().SetLetter(GetAssetOfLetter(myJSONInventer.GetBoard().letters[k].letter));
                        box.transform.localScale = cell.transform.localScale;
                        box.transform.parent = cell.transform;
                        
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
            string assetName = letterAssets[i].name;
            if(assetName.Equals("Ş") && letter.Equals("Ş"))
                return letterAssets[i];
            if(assetName.Equals("İ") && letter.Equals("İ"))
                return letterAssets[i];
            if(assetName.Equals("Ç") && letter.Equals("Ç"))
                return letterAssets[i];
            if(assetName.Equals("ğ") && letter.Equals("Ğ"))
                return letterAssets[i];
            if (assetName.Equals("Ö") && letter.Equals("Ö"))
                return letterAssets[i];
            if (assetName.Equals("Ü") && letter.Equals("Ü"))
                return letterAssets[i];
            if (assetName.Equals(letter))
            {
                return letterAssets[i];
            }
        }
        return null;
    }
    
}