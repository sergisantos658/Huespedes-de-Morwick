using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // minigame is for custom types, for example connect wires to interact with doors
    public enum InteractionType
    {
        Click,
        Hold,
        Minigame
    }

    [SerializeField] private float holdTime;

    [SerializeField] private bool interactOpction;

    [SerializeField] private bool observationOpction;

    public InteractionType interactionType;

    public bool InteractOpction => interactOpction;

    public bool ObservationOpction => observationOpction;

    public abstract void Interact();

    public abstract void Observation();

    public float GetHoldTime() => holdTime;
}
