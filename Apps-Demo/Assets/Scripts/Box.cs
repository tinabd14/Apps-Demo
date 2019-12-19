using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    bool touched;
    string letter;
    public void SetLetterAsset(Sprite letterAsset)
    {
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = letterAsset;
    }

    public string GetLetter()
    {
        return letter;
    }
    public void SetLetter(string letter)
    {
        this.letter = letter;
    }
    public void Touched()
    {
        touched = true;
    }

    public bool GetTouched()
    {
        return touched;
    }

    public void SetTouched(bool touch)
    {
        touched = touch;
    }


}
