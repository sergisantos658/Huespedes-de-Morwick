using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TriggerGetTransformToCamera : MonoBehaviour
{
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 rotation;

    [Space(20)]

    [SerializeField] private Transform mainCamera;

    public void OnValidate()
    {
        if (EditorApplication.isPlaying == false || !Application.isPlaying) { 
        
            mainCamera.transform.position = position;
            mainCamera.transform.rotation = Quaternion.Euler(rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            mainCamera.transform.position = position;
            mainCamera.transform.rotation = Quaternion.Euler(rotation);
        }
        
    }

}

[CustomEditor(typeof(TriggerGetTransformToCamera))]
public class TriggerGetCameraEditor : Editor
{
    TriggerGetTransformToCamera tGetCamera;

    private void OnEnable()
    {
        tGetCamera = (TriggerGetTransformToCamera)target; 
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Refresh"))
        {
            tGetCamera.OnValidate();
        }
    }

}