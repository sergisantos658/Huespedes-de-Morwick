using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    ControlObjects controlObjects;

    void awake()
    {
        controlObjects = GetComponent<ControlObjects>();
    }

    void OnMouseDown()
    {
        controlObjects.AddObject(transform.gameObject);
    }
}
