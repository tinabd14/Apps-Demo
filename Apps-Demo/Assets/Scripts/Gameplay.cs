using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public static JSONInventer myJSONInventer;
    
    List<GameObject> selectedBoxes = new List<GameObject>();
    [SerializeField] List<GameObject> allBoxes = new List<GameObject>();
    List<GameObject> cells = new List<GameObject>();
    [SerializeField] List<string> words = new List<string>();


    private void Start()
    {
        myJSONInventer = new JSONInventer();
        allBoxes = Grid.boxes;
        cells = Grid.cells;
        foreach (var word in myJSONInventer.GetWords())
            words.Add(word as string);
    }

    private void Update()
    {
        CheckIfWon();
        SelectBoxes();
    }

    private void CheckIfWon()
    {
        if(words.Count == 0)
        {
            Debug.Log("WON!");
        }
    }

    private void DropBoxesIfPossible()
    {
        for(int i = 0; i < allBoxes.Count; i++)
        {
            GameObject candidateToDrop = allBoxes[i];
            if (candidateToDrop != null)
            {
                int colIndex = candidateToDrop.transform.parent.GetComponent<Cell>().GetColIndex();

                for (int j = candidateToDrop.transform.parent.GetComponent<Cell>().GetRowIndex(); j >= 0; j--)
                {
                    Debug.Log("j: " + j + "col: " + candidateToDrop.transform.parent.GetComponent<Cell>().GetColIndex());
                    Debug.Log(Grid.GetCellAt(j, colIndex).transform.GetChild(0).name);
                    if (!(Grid.GetCellAt(j, colIndex).transform.GetChild(0).name.Equals("Box 1(Clone)")))
                    {
                        Debug.Log("HAHHAHA");
                        candidateToDrop.transform.SetParent(cells[j].transform);
                    }
                }
            }
        }
    }

    private void SelectBoxes()
    {
        GameObject nextSelected = EventSystem.current.currentSelectedGameObject;
        if (!allBoxes.Contains(nextSelected))
        {
            nextSelected = null;
        }

        if (selectedBoxes.Count == 0 && nextSelected != null)
        {
            nextSelected.GetComponent<Image>().color = Color.red;
            selectedBoxes.Add(nextSelected);
        }
        else if(selectedBoxes.Count > 0 && nextSelected != null)
        {
            if ((selectedBoxes[selectedBoxes.Count - 1].transform.parent.GetComponent<Cell>().GetNeighbourCells().Contains(nextSelected.transform.parent.gameObject)) && !selectedBoxes.Contains(nextSelected))
            {
                nextSelected.GetComponent<Image>().color = Color.red;
                selectedBoxes.Add(nextSelected);
            }
        }

    }

    public void Deselect()
    {
        selectedBoxes.Clear();
        for (int i = 0; i < allBoxes.Count; i++)
        {
            allBoxes[i].GetComponent<Box>().SetTouched(false);
            allBoxes[i].GetComponent<Image>().color = Color.white;
        }
    }

    public void Check()
    {
        string word = "";
        for(int i = 0; i < selectedBoxes.Count; i++)
        {
            word = word + selectedBoxes[i].GetComponent<Box>().GetLetter();
        }

        if(words.Contains(word))
        {
            for(int i = 0; i < selectedBoxes.Count; i++)
            {
                allBoxes[allBoxes.IndexOf(selectedBoxes[i])] = null;
                Destroy(selectedBoxes[i]);
            }
            words.Remove(word);
            DropBoxesIfPossible();
        }
    }

}
