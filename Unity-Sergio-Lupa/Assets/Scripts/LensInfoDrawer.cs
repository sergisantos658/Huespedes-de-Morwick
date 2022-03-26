using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LensInfoDrawer : MonoBehaviour
{
    public void OnRenderObject()
    {
        DissolvantManager.RenderLenses();
    }
}