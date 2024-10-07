using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Harpoon : MonoBehaviour
{
    [SerializeField] private GameObject body_and_head;
    [SerializeField] private GameObject R_shoulder;
    [SerializeField] private GameObject harpoon;
    
    private List<Collider2D> the_sea_colliders;
    private Collider2D harpoon_head_collider;
    private The_Old_Man the_old_man;
    
    public bool is_attacking = false;

    private void Start()
    {
        the_sea_colliders = new List<Collider2D>();
        GameObject.Find("Head").TryGetComponent(out harpoon_head_collider);
        TryGetComponent(out the_old_man);
    }

    public void Start_Action_Harpoon()
    {
        StopCoroutine(Action_Harpoon_Co());
        StartCoroutine(Action_Harpoon_Co());
    }

    private IEnumerator Action_Harpoon_Co()
    {
        is_attacking = true;
        int frame_count=0;
        WaitForSeconds wait_for_1_frame = new WaitForSeconds(1f/60f);
        WaitForSeconds wait_for_9_frame = new WaitForSeconds(0.15f);
        while (frame_count <= 3)
        {
            frame_count++;
            On_Attack();
            body_and_head.transform.localRotation = Quaternion.AngleAxis(0f + frame_count * 40f / 3f, Vector3.forward);
            R_shoulder.transform.localRotation = Quaternion.AngleAxis(-30f - frame_count * 10f / 3f, Vector3.forward);
            harpoon.transform.localRotation = Quaternion.AngleAxis(30f - frame_count * 20f / 3f, Vector3.forward);
            yield return wait_for_1_frame;
        }
        frame_count = 0;
        yield return wait_for_9_frame;
        body_and_head.transform.localRotation = Quaternion.AngleAxis(0f, Vector3.forward);
        R_shoulder.transform.localRotation = Quaternion.AngleAxis(-30f, Vector3.forward);
        harpoon.transform.localRotation = Quaternion.AngleAxis(30f, Vector3.forward);
        yield return wait_for_9_frame;
        is_attacking = false;
    }

    private void On_Attack()
    {
        Physics2D.OverlapCollider(harpoon_head_collider, new ContactFilter2D().NoFilter(), the_sea_colliders);
        if (the_sea_colliders != null)
            for (int i = 0; i < the_sea_colliders.Count; i++)
                if (the_sea_colliders[i].CompareTag("The_Sea"))
                {
                    the_sea_colliders[i].transform.GetComponent<The_Sea>().Take_Damage(the_old_man.attack_damage_get);
                    the_sea_colliders[i].attachedRigidbody.AddForce((the_sea_colliders[i].transform.position - harpoon_head_collider.transform.position).normalized * 20f, ForceMode2D.Force);
                }
    }
}
