using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraManager : MonoBehaviour
{
    public List<Transform> camPos = new List<Transform>();

    public int currentPosition = 0;

    public int lastPosition = 0;

    public static CameraManager camManager;
    public Camera cam;

    
    void Awake()
    {
        cam = GetComponent<Camera>();

        camManager = this;
    }

    public static void SetCameraPosition(int index)
    {
        camManager.lastPosition = camManager.currentPosition;
        camManager.currentPosition = index;
        camManager.cam.transform.position = camManager.camPos[index].position;
    }

    public static void SetCameraPosition(Transform transform)
    {
        SetCameraPosition(FindIndex(transform));
    }

    public static int FindIndex(Transform transform)
    {
        for (int i = 0; i < camManager.camPos.Count; i++)
        {
            if (camManager.camPos[i] == transform)
            {
                return i;
            }
        }

        Debug.LogError(transform.name + " No se encuentra en la lista");

        return -1;
    }

    public static void returnToLastPos()
    {
        SetCameraPosition(camManager.lastPosition);
    }
}
