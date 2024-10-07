using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Old_Man : MonoBehaviour
{
    [SerializeField] private float attack_damage;
    public float attack_damage_get => attack_damage;

    [SerializeField] private float attack_speed;
    public float attack_speed_get => attack_speed;
    
    [SerializeField] private float health_max;
    public float health_max_get => health_max;

    private float health_current;
    public float health_current_get => health_current;

    public bool is_died = false;

    private SpriteRenderer renderer_body_and_head;
    private SpriteRenderer renderer_L_arm;
    private SpriteRenderer renderer_L_shoulder;
    private SpriteRenderer renderer_L_hand;
    private SpriteRenderer renderer_R_arm;
    private SpriteRenderer renderer_R_shoulder;
    private SpriteRenderer renderer_R_hand;

    private void Awake()
    {
        GameObject.Find("Body&Head").TryGetComponent(out renderer_body_and_head);
        GameObject.Find("L_Arm").TryGetComponent(out renderer_L_arm);
        GameObject.Find("L_Shoulder").TryGetComponent(out renderer_L_shoulder);
        GameObject.Find("L_Hand").TryGetComponent(out renderer_L_hand);
        GameObject.Find("R_Arm").TryGetComponent(out renderer_R_arm);
        GameObject.Find("R_Shoulder").TryGetComponent(out renderer_R_shoulder);
        GameObject.Find("R_Hand").TryGetComponent(out renderer_R_hand);

        renderer_body_and_head.color = Color.white;
        renderer_L_arm.color = Color.white;
        renderer_L_shoulder.color = Color.white;
        renderer_L_hand.color = Color.white;
        renderer_R_arm.color = Color.white;
        renderer_R_shoulder.color = Color.white;
        renderer_R_hand.color = Color.white;

        health_current = health_max;
    }

    public void Take_Damage(float damage)
    {
        health_current -= damage;
        StopCoroutine(Hit_Animation_Co());
        StartCoroutine(Hit_Animation_Co());
        if (health_current <= 0 && !is_died)
        {
            is_died = true;
            StartCoroutine(Die_Animation_Co());
        }
    }

    public void Heal(float heal_amount)
    {
        health_current = Mathf.Min(health_max, health_current + heal_amount);
    }

    private IEnumerator Hit_Animation_Co()
    {
        renderer_body_and_head.color = Color.red;
        renderer_L_arm.color = Color.red;
        renderer_L_shoulder.color = Color.red;
        renderer_L_hand.color = Color.red;
        renderer_R_arm.color = Color.red;
        renderer_R_shoulder.color = Color.red;
        renderer_R_hand.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer_body_and_head.color = Color.white;
        renderer_L_arm.color = Color.white;
        renderer_L_shoulder.color = Color.white;
        renderer_L_hand.color = Color.white;
        renderer_R_arm.color = Color.white;
        renderer_R_shoulder.color = Color.white;
        renderer_R_hand.color = Color.white;
    }

    private IEnumerator Die_Animation_Co()
    {
        int frame_count = 0;
        WaitForSeconds wait_for_1_frame = new WaitForSeconds(1f / 60f);
        while (frame_count < 40)
        {
            this.transform.localScale = new Vector3(Mathf.Max(this.transform.localScale.x - 1f/30f, 0), Mathf.Max(this.transform.localScale.y - 1f/30f, 0), this.transform.localScale.z);
            frame_count++;
            yield return wait_for_1_frame;
        }
        this.transform.localScale = new Vector3(0f, 0f, this.transform.localScale.z);
        GameManager.instance.The_Sail_Over();
    }
}
