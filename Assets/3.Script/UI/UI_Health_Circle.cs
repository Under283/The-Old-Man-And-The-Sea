using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health_Circle : MonoBehaviour
{
    public Vector2 entity_position;

    private GameObject entity;
    private The_Sea entity_the_sea;
    [SerializeField] private Image circle_fill;
    [SerializeField] private Text health;

    public void UI_Health_Circle_Setup(GameObject entity)
    {
        this.entity = entity;
        entity.TryGetComponent(out entity_the_sea);
    }

    void Update()
    {
        if (entity_the_sea.health_current_get <= 0)
        {
            Destroy(gameObject);
            return;
        }

        entity_position = Camera.main.WorldToScreenPoint(entity.transform.position);
        this.transform.position = entity_position;
        circle_fill.fillAmount = entity_the_sea.health_current_get / entity_the_sea.health_max_get;
        health.text = "" + (int)entity_the_sea.health_current_get;
    }
}
