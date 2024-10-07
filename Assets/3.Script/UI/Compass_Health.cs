using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass_Health : MonoBehaviour
{
    [SerializeField] private Image health_circle;
    [SerializeField] private Text health_text;

    [SerializeField] private GameObject the_old_man_object;
    private The_Old_Man the_old_man;

    private void Awake()
    {
        the_old_man_object.TryGetComponent(out the_old_man);
    }

    private void Update()
    {
        if (the_old_man.is_died)
        {
            health_circle.fillAmount = 0f;
            health_text.text = "0";
            return;
        }
        health_circle.fillAmount = the_old_man.health_current_get / the_old_man.health_max_get;
        health_text.text = "" + (int)the_old_man.health_current_get;
    }
}
