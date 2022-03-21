using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class SphereMaskController : MonoBehaviour
{
    public float radius = 0.5f;
    public float length = 0.5f;
    public Transform pivot;
    void OnRenderObject()
    {
        if(pivot)
        {
            DissolvantManager.SetLens(this.GetInstanceID().ToString(), (pivot.position - transform.position).normalized, transform.position, radius, Vector3.Distance(pivot.position,transform.position));
        }
        else
        {
            DissolvantManager.SetLens(this.GetInstanceID().ToString(), transform.forward, transform.position, radius, length);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + transform.forward * length, radius);
        /*Gizmos.DrawLine(transform.position, transform.position + new Vector3(radius, 0, length));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-radius, 0, length));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, radius, length));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -radius, length));*/
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * length);

    }
}
