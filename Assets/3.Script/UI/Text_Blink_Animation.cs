using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Blink_Animation : MonoBehaviour
{
    private float time;
    Text text;

    private void Awake()
    {
        TryGetComponent(out text);
    }

    private void Update()
    {
        if(time < 0.5f)text.color = new Color(text.color.r, text.color.g, text.color.b, 1 - time);
        else
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, time);
            if(time > 1f) time = 0;
        }
        time += Time.deltaTime;
    }
}
