using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateNpc : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        DialogueActivate.LookPlayer += LookPlayer;
    }

    private void OnDisable()
    {
        DialogueActivate.LookPlayer -= LookPlayer;
    }

    private void LookPlayer(PlayerController target)
    {
        Vector3 relativePos = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(-relativePos);
        transform.rotation = rotation;
    }
}
