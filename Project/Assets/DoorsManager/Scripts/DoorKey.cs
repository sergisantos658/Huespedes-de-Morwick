using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public bool isRedKey = true;
    PlayerInteraction player;

    float distance;
    float angleView;
    Vector3 direction;

    private void Start()
    {
        player = FindObjectOfType<PlayerInteraction>();
    }

    void Update()
    {
        if ( NearView() && Input.GetKeyDown(KeyCode.E) )
        {
            if (isRedKey) player.RedKey = true;
            else player.BlueKey = true;
            Destroy(gameObject);
        }
    }

    bool NearView()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);
        if (distance < 2f) return true;
        else return false;
    }
}
