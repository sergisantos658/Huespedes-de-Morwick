using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour
{
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = ray.GetPoint(5);
        transform.position = point;
        print(point);
        //transform.localScale = Vector3.one - new Vector3(Mathf.Abs(transform.localPosition.normalized.x), Mathf.Abs(transform.localPosition.normalized.y), Mathf.Abs(transform.localPosition.normalized.z)) * scaleFactor * transform.localPosition.magnitude;
    }
}
