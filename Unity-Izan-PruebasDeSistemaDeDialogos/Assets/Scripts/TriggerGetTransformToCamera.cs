using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGetTransformToCamera : MonoBehaviour
{
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 rotation;

    [Space(20)]

    [SerializeField] private Transform mainCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            mainCamera.transform.position = position;
            mainCamera.transform.rotation = Quaternion.Euler(rotation);
        }
        
    }

}
