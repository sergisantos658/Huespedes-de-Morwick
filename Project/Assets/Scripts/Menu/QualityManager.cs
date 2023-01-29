using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QualityManager : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int quality;

    void Start()
    {
        quality = PlayerPrefs.GetInt("qualityNumber", 3);
        dropdown.value = quality;
        SetQuality();
    }

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("qualityNumber", dropdown.value);
        quality = dropdown.value;
    }
}
