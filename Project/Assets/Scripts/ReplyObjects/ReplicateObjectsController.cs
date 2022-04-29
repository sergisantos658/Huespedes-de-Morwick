using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public struct SettingsOfObject
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 escale;
}

public class ReplicateObjectsController : MonoBehaviour
{
    [System.Serializable]
    public class ObjectToReplicate
    {
        public GameObject prefab;
        [HideInInspector] public Mesh mesh;
        [HideInInspector] public Material[] materials;
        public SettingsOfObject[] settings;

        [HideInInspector] public List<Matrix4x4> matrix = new List<Matrix4x4>();
    }

    public ObjectToReplicate[] objects;

    void Start()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].mesh = objects[i].prefab.GetComponent<MeshFilter>().sharedMesh;

            objects[i].materials = objects[i].prefab.GetComponent<MeshRenderer>().sharedMaterials;

            for (int o = 0; o < objects[i].mesh.subMeshCount; o++)
            { 
                Debug.Log("Mesh: " + " " + i + " " + objects[i].mesh + " material " + objects[i].materials[o]);
            }

            for (int o = 0; o < objects[i].settings.Length; o++)
            {
                Matrix4x4 objectMatrix = Matrix4x4.Translate(objects[i].settings[o].position) * Matrix4x4.Rotate(Quaternion.Euler(objects[i].settings[o].rotation)) *
                Matrix4x4.Scale(objects[i].settings[o].escale);

                objects[i].matrix.Add(objectMatrix);
            }

        }
    }

    void Update()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            for (int o = 0; o < objects[i].mesh.subMeshCount; o++)
            {
                Graphics.DrawMeshInstanced(objects[i].mesh, o, objects[i].materials[o], objects[i].matrix);

            }
        }
        
    }
}


[CustomEditor(typeof(ReplicateObjectsController)), CanEditMultipleObjects]
public class FreeMoveHandleExampleEditor : Editor
{
    ReplicateObjectsController rObjects;

    private void OnEnable()
    {
        rObjects = (ReplicateObjectsController)target;

        for (int i = 0; i < rObjects.objects.Length; i++)
        {
            if(EditorApplication.isPlaying == false)
            {
                rObjects.objects[i].mesh = rObjects.objects[i].prefab.GetComponent<Mesh>();
            }
            
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


    }

    void OnSceneGUI()
    {
        if (EditorApplication.isPlaying == false || !Application.isPlaying)
        {
            for (int i = 0; i < rObjects.objects.Length; i++)
            {
                for (int o = 0; o < rObjects.objects[i].settings.Length; o++)
                {

                    rObjects.objects[i].settings[o].position = Handles.PositionHandle(rObjects.objects[i].settings[o].position, Quaternion.Euler(rObjects.objects[i].settings[o].rotation) );

                    rObjects.objects[i].settings[o].rotation = Handles.RotationHandle(Quaternion.Euler(rObjects.objects[i].settings[o].rotation), rObjects.objects[i].settings[o].position).eulerAngles;



                }
            }
        }
    }

}