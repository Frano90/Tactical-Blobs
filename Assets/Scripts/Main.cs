using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main instance;

    public UnitController unitController; 
    public GridController gridController; 

    private void Awake()
    {
        if (instance == null) instance = this;
    }

}
