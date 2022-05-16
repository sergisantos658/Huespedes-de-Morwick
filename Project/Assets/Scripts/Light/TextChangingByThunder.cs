using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextChangingByThunder : MonoBehaviour
{
    [SerializeField] private LightsSwitch switchLight;
    [SerializeField] private ThunderController thunderC;

    TextMeshPro m_text;

    void Start()
    {
        m_text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (switchLight.allLightsOff)
        {
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b,
                (thunderC.ThunderLight.intensity / thunderC.Intensity));

        }
        else
        {
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b,
                0);
        }


    }
}
