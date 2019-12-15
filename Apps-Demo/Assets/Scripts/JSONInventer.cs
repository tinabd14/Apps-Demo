using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONInventer
{
    private string jsonString;
    private string topic;
    private string[] words;
    private double rewardCoin;
    private Board board;

    Puzzle puzzleJSON;
    public JSONInventer()
    {
        InitializeVariablesFromJSON(puzzleJSON);
    }

    private void InitializeVariablesFromJSON(Puzzle puzzleJSON)
    {
        jsonString = File.ReadAllText(Application.dataPath + "/Resources/Puzzle.json");
        puzzleJSON = JsonUtility.FromJson<Puzzle>(jsonString);

        topic = puzzleJSON.topic;
        words = puzzleJSON.words;
        rewardCoin = puzzleJSON.rewardCoin;
        Board board = puzzleJSON.board;
    }

    public string GetTopic()
    {
        return topic;
    }

    public string[] GetWords()
    {
        return words;
    }

    public double GetRewardCoin()
    {
        return rewardCoin;
    }

    public Board GetBoard()
    {
        return board;
    }



    [Serializable]
    public class Puzzle
    {
        public string topic = null;
        public string[] words = null;
        public double rewardCoin = 0;
        public Board board;
    }

    [Serializable]
    public class Board
    {
        public int boardRow;
        public int boardCol;
        public Letter[] letters;
    }

    [Serializable]
    public class Letter
    {
        public int rowIndex;
        public int colIndex;
        public string letter;
    }

}
