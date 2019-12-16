using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    JSONInventer myJSONinventer;
    public static List<Sprite> letterAssets = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        myJSONinventer = new JSONInventer();
        GetAllLettersFromAssets();
        PrepareGrid();
    }


    private void PrepareGrid()
    {
        int boardRow = myJSONinventer.GetBoard().boardRow;
        int boardCol = myJSONinventer.GetBoard().boardCol;

        for(int i = 0; i < boardRow * boardCol; i++)
        {
            string letter = FindLetter(i);
            int rowIndex = FindRowIndex(i);
            int colIndex = FindColIndex(i);

            GameObject box = GameObject.Find("Box");
            box.GetComponent<Box>().ReScaleBox(boardRow, boardCol);
            box.GetComponent<Box>().SetLetter(GetAssetOfLetter(letter));
            box.GetComponent<Box>().SetPosition(rowIndex, colIndex);
        }
    }

    private int FindColIndex(int i)
    {
        return myJSONinventer.GetBoard().letters[i].colIndex;
    }

    private int FindRowIndex(int i)
    {
        return myJSONinventer.GetBoard().letters[i].rowIndex;
    }

    private string FindLetter(int i)
    {
        return myJSONinventer.GetBoard().letters[i].letter;
    }

    private List<Sprite> GetAllLettersFromAssets()
    {
        letterAssets.Clear();
        var sprites = Resources.LoadAll("alfabe", typeof(Sprite));
        foreach (var sprite in sprites)
            letterAssets.Add(sprite as Sprite);
        return letterAssets;
    }

    private Sprite GetAssetOfLetter(string letter)
    {
        for(int i = 0; i < letterAssets.Count; i++)
        {
            if (letterAssets[i].name.Equals(letter))
                return letterAssets[i];
        }
        return null;
    }

}
