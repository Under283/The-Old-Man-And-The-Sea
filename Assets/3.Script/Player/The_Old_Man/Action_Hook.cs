using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Hook : MonoBehaviour
{
    [SerializeField] private Rigidbody2D the_sail;
    [SerializeField] public Rigidbody2D the_sea;
    [SerializeField] private LineRenderer rope;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform hook;
    
    public bool is_launching = false;
    public bool is_attatched = false;
    
    void Start()
    {
        rope.positionCount = 2;
        rope.endWidth = rope.startWidth = 0.02f;
        rope.SetPosition(0, muzzle.position);
        rope.SetPosition(1, hook.position);
        rope.useWorldSpace = true;
    }
    public void Start_Action_Hook_Launch()
    {
        StartCoroutine(Hook_Launch_Co());
    }

    private IEnumerator Hook_Launch_Co()
    {
        is_launching = true;
        hook.position = muzzle.position;
        Vector2 cursor_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        int frame_count = 0;
        WaitForSeconds wait_for_1_frame = new WaitForSeconds(1f/60f);
        WaitForEndOfFrame wait_for_end_of_frame = new WaitForEndOfFrame();
        while(is_launching)
        {
            yield return wait_for_end_of_frame;
            frame_count++;
            if (frame_count < 30 && !is_attatched)
            {
                rope.SetPosition(0, muzzle.position);
                rope.SetPosition(1, hook.position);
                hook.position = Vector2.MoveTowards(hook.position, cursor_position, frame_count * 0.013f);
                yield return wait_for_1_frame;
            }
            else if (frame_count >= 30 && !is_attatched)
            {
                rope.SetPosition(0, muzzle.position);
                rope.SetPosition(1, hook.position);
                hook.position = Vector2.MoveTowards(hook.position, muzzle.position, frame_count * 0.013f);
                yield return wait_for_1_frame;
                if (Vector2.Distance(hook.position, muzzle.position) < 0.01f) is_launching = false;
            }
            else if (is_attatched)
            {
                rope.SetPosition(0, muzzle.position);
                rope.SetPosition(1, hook.position);
                hook.position = Vector2.MoveTowards(hook.position, muzzle.position, frame_count * 0.005f);
                the_sail.AddForce((the_sea.transform.position - the_sail.transform.position).normalized * 20f, ForceMode2D.Force);
                the_sea.AddForce((muzzle.transform.position - the_sea.transform.position).normalized * 20f, ForceMode2D.Force);
                yield return wait_for_1_frame;
                if (Vector2.Distance(hook.position, muzzle.position) < 0.01f)
                {
                    is_launching = false;
                    is_attatched = false;
                }
            }
        }

        hook.position = muzzle.position;
        rope.SetPosition(0, muzzle.position);
        rope.SetPosition(1, hook.position);

        yield return null;
    }
}
