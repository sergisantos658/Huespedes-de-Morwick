using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateNpc : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 1;
    PlayerController player;
    bool interact;
    Quaternion originalRot;
    float temp = 0;
    private void OnEnable()
    {
        GetComponent<DialogueActivate>().LookPlayer += LookPlayer;
    }

    private void OnDisable()
    {
        GetComponent<DialogueActivate>().LookPlayer -= LookPlayer;
    }

    private void LookPlayer(PlayerController target)
    {
        originalRot = transform.rotation;
        player = target;
        interact = true;
        temp = 1f;
        
        
    }

    private void Update()
    {
        if(player != null)
        {
            if (temp <= 0)
            {
                if (interact == true && !player.DialogueUI.isOpen)
                {
                    interact = false;
                    temp = 1f;
                    Debug.Log("hola");
                }
            }
            else
            {
                temp -= Time.deltaTime;

            }

            if (!player.DialogueUI.isOpen && interact == false && temp > 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, originalRot, speed * Time.deltaTime);

            }

            if (interact == true)
            {
                Vector3 relativePos = player.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(-relativePos);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
            }
        }


    }
}
