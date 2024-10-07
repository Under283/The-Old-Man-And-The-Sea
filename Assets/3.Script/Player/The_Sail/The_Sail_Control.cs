using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Sail_Control : MonoBehaviour
{
    
    private Rigidbody2D the_sail_rigidbody2D;

    private The_Sail the_sail;
    private The_Old_Man the_old_man;
    
    private float force;
    private float torque;
    private float last_dash;

    private float map_size;
    private float clamp_x;
    private float clamp_y;


    private void Awake()
    {
        map_size = GameObject.Find("The_Ocean").GetComponent<Map_Generator>().mapSize;
        GameObject.Find("The_Old_Man").TryGetComponent(out the_old_man);
        TryGetComponent(out the_sail);
        the_sail_rigidbody2D = GetComponent<Rigidbody2D>();
        the_sail_rigidbody2D.drag = the_sail.linear_drag_get;
        the_sail_rigidbody2D.angularDrag = the_sail.angular_drag_get;
    }

    private void Update()
    {
        if (the_old_man.is_died) return;

        torque = Input.GetAxis("Horizontal");
        force = Input.GetAxis("Vertical");

        the_sail_rigidbody2D.AddTorque(torque * the_sail.torque_rate_get, ForceMode2D.Force);
        the_sail_rigidbody2D.AddForce(transform.up * force * the_sail.force_rate_get, ForceMode2D.Force);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= last_dash + the_sail.dash_cooldown_get)
        {
            the_sail_rigidbody2D.AddForce(transform.up * the_sail.force_rate_get * 100f, ForceMode2D.Force);
            last_dash = Time.time;
        }

        the_sail_rigidbody2D.velocity -= (Vector2)(gameObject.transform.rotation.eulerAngles * the_sail_rigidbody2D.velocity.magnitude);

        clamp_x = Mathf.Clamp(transform.position.x, -(map_size / 2), map_size / 2);
        clamp_y = Mathf.Clamp(transform.position.y, -(map_size / 2), map_size / 2);
        transform.position = new Vector3(clamp_x, clamp_y, 0f);

    }
}
