using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class FadeIn : MonoBehaviour
{
    // Start is called before the first frame update
    Image render;
    public ChangeScene change;
    void Start()
    {
        render = GetComponent<Image>();
        Color c = render.color;
        c.a = 0f;
        render.color = c;
        StartCoroutine(fadein());
    }

    IEnumerator fadein()
    {
        for(float alf = 0.05f; alf <= 1.1; alf += 0.05f)
        {
            Color c = render.color;
            c.a = alf;
            render.color = c;
            yield return new WaitForSeconds(0.05f);

        }
        Cursor.visible = true;
        MorwickController.MorwickCauch = false;
        PlayerCheckPoint.Instance.CheckPointSave();
        change.SceneLoad();
    }

    // Update is called once per frame

}
