using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TriggerGetTransformToCamera : MonoBehaviour
{
	public Transform posCamera;

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<PlayerController>())
		{
			ChangePos();
		}
		
	}

	public void ChangePos()
	{
		CameraManager.SetCameraPosition(posCamera);
	}
}

#if UNITY_EDITOR
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

		if (GUILayout.Button("SetPosition"))
		{
			tGetCamera.ChangePos();
		}
	}

}

#endif