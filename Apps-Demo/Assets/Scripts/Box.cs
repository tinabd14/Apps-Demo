using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{    
    public void SetLetter(Sprite letterAsset)
    {
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = letterAsset;
    }

}
