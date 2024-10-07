using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Old_Man_Control : MonoBehaviour
{
    Action_Harpoon action_harpoon;
    Action_Hook action_hook;
    The_Old_Man the_old_man;

    float angle;
    Vector2 cursor;

    float last_attack_time=0;

    private void Start()
    {
        TryGetComponent(out action_harpoon);
        TryGetComponent(out action_hook);
        TryGetComponent(out the_old_man);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.Menu_ESC_Open();
        }
        else if (Input.GetMouseButton(0) && Time.time >= last_attack_time + the_old_man.attack_speed_get && !action_hook.is_launching)
        {
            cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(cursor.y - this.transform.position.y, cursor.x - this.transform.position.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            last_attack_time = Time.time;
            action_harpoon.Start_Action_Harpoon();
        }
        else if(Input.GetMouseButton(1) && !action_hook.is_launching && !action_harpoon.is_attacking)
        {
            cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(cursor.y - this.transform.position.y, cursor.x - this.transform.position.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            action_hook.Start_Action_Hook_Launch();
        }
        else if (!action_harpoon.is_attacking && !action_hook.is_launching)
        {
            cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(cursor.y - this.transform.position.y, cursor.x - this.transform.position.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
