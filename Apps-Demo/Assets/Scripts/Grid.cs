using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private int rowCount;
    private int colCount;

    [SerializeField] GameObject cellPrefab;
    [SerializeField] GameObject boxPrefab;
    private List<Sprite> letterAssets;

    JSONInventer myJSONInventer;
    public static List<GameObject> boxes = new List<GameObject>();
    public static List<GameObject> cells = new List<GameObject>();


    void Start()
    {
        InitializeJSONInventer();
        InitializeGridProperties();
        FillWithCells();
        foreach (var cell in cells)
            FindNeighbourCells(cell.GetComponent<Cell>());
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
        if (Gameplay.myJSONInventer == null)
        {
            myJSONInventer = new JSONInventer();
        }
        else
        {
            myJSONInventer = Gameplay.myJSONInventer;
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
                        GameObject cell = Instantiate(cellPrefab,transform);
                        cell.GetComponent<Cell>().Initialize();
                        cell.GetComponent<Cell>().SetPosition(i, j);
                        
                        GameObject box = Instantiate(boxPrefab, cell.transform);
                        box.GetComponent<Box>().SetLetter(myJSONInventer.GetBoard().letters[k].letter);
                        box.GetComponent<Box>().SetLetterAsset(GetAssetOfLetter(myJSONInventer.GetBoard().letters[k].letter));
                        box.GetComponent<RectTransform>().sizeDelta = cell.GetComponent<RectTransform>().sizeDelta;
                        cell.GetComponent<Cell>().SetChildBox(box);
                        
                        cells.Add(cell);
                        boxes.Add(box);
                    }
                }
            }
        }
    }

    public static GameObject GetCellAt(int row, int col)
    {
        Vector2 tmp = new Vector2(row, col);
        for(int i = 0; i < cells.Count; i++)
        {
            if (cells[i].GetComponent<Cell>().GetRowCol().Equals(tmp))
                return cells[i];
        }
        return null;
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

    
    private void FindNeighbourCells(Cell cell)
    {
        int upperNeighbourRow = cell.GetRowIndex() + 1;
        int upperNeighbourCol = cell.GetColIndex();
        Vector2 upper = new Vector2(upperNeighbourRow, upperNeighbourCol);

        int bottomNeighbourRow = cell.GetRowIndex() - 1;
        int bottomNeighbourCol = cell.GetColIndex();
        Vector2 bottom = new Vector2(bottomNeighbourRow, bottomNeighbourCol);

        int leftNeighbourRow = cell.GetRowIndex();
        int leftNeighbourCol = cell.GetColIndex() - 1;
        Vector2 left = new Vector2(leftNeighbourRow, leftNeighbourCol);

        int rightNeighbourRow = cell.GetRowIndex();
        int rightNeighbourCol = cell.GetColIndex() + 1;
        Vector2 right = new Vector2(rightNeighbourRow, rightNeighbourCol);


        List<GameObject> neighbours = new List<GameObject>();
        foreach (var aCell in cells)
        {
            if (aCell.GetComponent<Cell>().GetRowCol().Equals(upper) || aCell.GetComponent<Cell>().GetRowCol().Equals(bottom) || aCell.GetComponent<Cell>().GetRowCol().Equals(left) || aCell.GetComponent<Cell>().GetRowCol().Equals(right))
            {
                neighbours.Add(aCell);
            }
        }

        cell.SetNeighbourCells(neighbours);

    }
}