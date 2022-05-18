
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemsData
{

    public string scriptedItemName;
    public bool pickUp = false;

    public ItemsData(Items items)
    {
        scriptedItemName = items.name;
        pickUp = items.pickUp;
    }
}