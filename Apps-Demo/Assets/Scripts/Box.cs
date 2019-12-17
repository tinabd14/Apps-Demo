using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    bool touched;
    public void SetLetter(Sprite letterAsset)
    {
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = letterAsset;
    }

    public void Touched()
    {
        touched = true;
        Debug.Log("AAAADADDDAD");
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
