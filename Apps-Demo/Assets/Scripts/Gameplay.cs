using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    JSONInventer myJSONinventer;
    // Start is called before the first frame update
    void Start()
    {
        myJSONinventer = new JSONInventer();
        PrepareGrid();
    }

    private void PrepareGrid()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
