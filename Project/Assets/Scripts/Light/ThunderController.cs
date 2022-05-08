using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderController : MonoBehaviour
{
    [SerializeField] private float thunderTimer;
    [SerializeField] private float intensity;

    [Space (20), Range(0.1f, 0.9f)]
    [SerializeField] private float umbral;
    
    [SerializeField] private Light thunderLight;

    private float timer;
    private float timerToThunder;
    private int thunderCount = 0;
    bool IsThunder = false;

    public float Intensity => intensity;
    public Light ThunderLight => thunderLight;

    void Start()
    {
        timerToThunder = thunderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timerToThunder -= Time.deltaTime * 2;

        if (IsThunder)
        {
            timer -= Time.deltaTime * intensity;
            thunderLight.intensity = timer;

            Debug.Log("intensity " + (thunderLight.intensity / intensity));

            if(thunderCount == 0 && thunderLight.intensity <= umbral) // Make thunder Twice
            {
                IsThunder = false;
                thunderCount++;
            }

            if(thunderLight.intensity <= umbral && thunderCount != 0) // Reset
            {
                timerToThunder = thunderTimer;
                thunderCount = 0;
                IsThunder = false;
            }

        }

        if(timerToThunder <= 0 && !IsThunder)
        {
            IsThunder = true;

            timer = intensity;
            thunderLight.intensity = intensity;
            
        }

    }
}
