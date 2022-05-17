using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Items : ScriptableObject
{
    public Sprite spriteInventory;
    public bool pickUp = false;
    public DialogueObject Observation;
}
