using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook_Attatch : MonoBehaviour
{
    Action_Hook action_hook;

    private void Start()
    {
        action_hook = GameObject.Find("The_Old_Man").GetComponent<Action_Hook>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("The_Sea"))
        {
            action_hook.is_attatched = true;
            action_hook.the_sea = collision.attachedRigidbody;
        }
    }

}
