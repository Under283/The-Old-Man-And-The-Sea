using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Sail : MonoBehaviour
{
    [SerializeField] private float dash_cooldown;
    public float dash_cooldown_get => dash_cooldown;
    
    [SerializeField] private float force_rate;
    public float force_rate_get => force_rate;

    [SerializeField] private float torque_rate;
    public float torque_rate_get => torque_rate;



    [SerializeField] private float linear_drag;
    public float linear_drag_get => linear_drag;

    [SerializeField] private float angular_drag;
    public float angular_drag_get => angular_drag;
}
