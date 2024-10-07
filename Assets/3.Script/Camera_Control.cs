using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    [SerializeField] private float camera_speed;

    private Transform the_sail_transform;
    private float map_size;
    private float clamp_x;
    private float clamp_y;

    private void Awake()
    {
        GameObject.Find("The_Sail").TryGetComponent(out the_sail_transform);
        this.map_size = GameObject.Find("The_Ocean").GetComponent<Map_Generator>().mapSize;
    }

    private void Update()
    {
        //Vector3 dir = the_old_man_transform.position - this.transform.position;
        //Vector3 move_vector = new Vector3(dir.x * camera_speed * Time.deltaTime, dir.y * camera_speed * Time.deltaTime, 0.0f);
        //this.transform.Translate(move_vector);

        transform.position = Vector3.Lerp(transform.position, the_sail_transform.position, Time.deltaTime * camera_speed);
        //transform.position = the_sail_transform.position;
        clamp_x = Mathf.Clamp(transform.position.x, -(map_size/2)+9, map_size/2-9);
        clamp_y = Mathf.Clamp(transform.position.y, -(map_size/2)+5, map_size/2-5);
        transform.position = new Vector3(clamp_x, clamp_y, -10f);
    }
}
