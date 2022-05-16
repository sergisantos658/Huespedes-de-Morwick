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
    public bool buu;
    private Quaternion rotation;
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
    }

    private void Update()
    {
        if(player != null)
        {

                if (interact == true && !player.DialogueUI.isOpen)
                {
                    interact = false;
                }
            
            else
            {
                temp -= Time.deltaTime;

            }

            if (!player.DialogueUI.isOpen && interact == false)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, originalRot, speed * Time.deltaTime);

            }

            if (interact == true)
            {
                Vector3 relativePos = player.transform.position - transform.position;
                if(buu == false)
                {
                   rotation = Quaternion.LookRotation(relativePos);
                }
                else
                {
                   rotation = Quaternion.LookRotation(-relativePos);
                }
                
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
            }
        }


    }
}
