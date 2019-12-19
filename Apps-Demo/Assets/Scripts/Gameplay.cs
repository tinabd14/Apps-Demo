using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public static JSONInventer myJSONInventer;

    List<GameObject> selectedBoxes = new List<GameObject>();
    List<GameObject> allBoxes = new List<GameObject>();
    List<GameObject> cells = new List<GameObject>();
    List<string> words = new List<string>();

    private bool won;
    [SerializeField] Text topicText = null;
    [SerializeField] Text selectionText = null;

    //[SerializeField] GameObject wonPanel;
    private void Start()
    {
        myJSONInventer = new JSONInventer();
        allBoxes = Grid.boxes;
        cells = Grid.cells;
        won = false;
        topicText.text = myJSONInventer.GetTopic();

        //wonPanel.SetActive(false);
        foreach (var word in myJSONInventer.GetWords())
            words.Add(word as string);
    }


    private void Update()
    {
        if(!won)
            CheckIfWon();
        SelectBoxes();
    }

    private void CheckIfWon()
    {
        if(words.Count == 0)
        {
            won = true;
            //wonPanel.SetActive(true);
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

                for (int rowIndex = candidateToDrop.transform.parent.GetComponent<Cell>().GetRowIndex(); rowIndex >= 0; rowIndex--)
                {
                    if (Grid.GetCellAt(rowIndex, colIndex).GetComponent<Cell>().GetChildBox() == null)
                    {
                        RepositionBox(candidateToDrop, rowIndex, colIndex);
                    }
                }
            }
        }
    }

    private void RepositionBox(GameObject candidateToDrop, int rowIndex, int colIndex)
    {
        candidateToDrop.transform.parent.GetComponent<Cell>().SetChildBox(null);
        candidateToDrop.transform.SetParent(Grid.GetCellAt(rowIndex, colIndex).transform);
        candidateToDrop.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Grid.GetCellAt(rowIndex, colIndex).GetComponent<Cell>().SetChildBox(candidateToDrop);
        allBoxes[allBoxes.IndexOf(candidateToDrop)] = null;
        allBoxes[cells.IndexOf(Grid.GetCellAt(rowIndex, colIndex))] = candidateToDrop;
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
            nextSelected.GetComponent<Image>().color = Color.green;
            selectionText.text = selectionText.text + nextSelected.GetComponent<Box>().GetLetter();
            selectedBoxes.Add(nextSelected);
        }
        else if(selectedBoxes.Count > 0 && nextSelected != null)
        {
            if ((selectedBoxes[selectedBoxes.Count - 1].transform.parent.GetComponent<Cell>().GetNeighbourCells().Contains(nextSelected.transform.parent.gameObject)) && !selectedBoxes.Contains(nextSelected))
            {
                nextSelected.GetComponent<Image>().color = Color.green;
                selectionText.text = selectionText.text + nextSelected.GetComponent<Box>().GetLetter();
                selectedBoxes.Add(nextSelected);
            }
        }

    }

    public void Deselect()
    {
        selectedBoxes.Clear();
        selectionText.text = "";
        for (int i = 0; i < allBoxes.Count; i++)
        {
            if(allBoxes[i] != null)
            {
                allBoxes[i].GetComponent<Box>().SetTouched(false);
                allBoxes[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void Check()
    {
        string word = "";
        for(int i = 0; i < selectedBoxes.Count; i++)
        {
            word += selectedBoxes[i].GetComponent<Box>().GetLetter();
        }

        if(words.Contains(word))
        {
            for(int i = 0; i < selectedBoxes.Count; i++)
            {
                selectedBoxes[i].transform.parent.GetComponent<Cell>().SetChildBox(null);
                Destroy(allBoxes[allBoxes.IndexOf(selectedBoxes[i])]);
                allBoxes[allBoxes.IndexOf(selectedBoxes[i])] = null;
            }
            words.Remove(word);
            selectedBoxes.Clear();
            selectionText.text = "";
            DropBoxesIfPossible();
        }
        else
        {
            Deselect();
        }
    }

}
