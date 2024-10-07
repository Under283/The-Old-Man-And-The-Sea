using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Sea : MonoBehaviour
{
    [SerializeField] private float attack_damage;
    public float attack_damage_get => attack_damage;

    [SerializeField] private float health_max;
    public float health_max_get => health_max;

    private float health_current;
    public float health_current_get => health_current;


    private SpriteRenderer renderer_default;
    private SpriteRenderer renderer_attack;
    private SpriteRenderer renderer_fin;

    private The_Old_Man the_old_man;

    private float map_size;
    private float clamp_x;
    private float clamp_y;

    Color white_albedo;
    Color red_albedo;

    private void Awake()
    {
        GameObject.Find("The_Old_Man").TryGetComponent(out the_old_man);
        transform.TryGetComponent(out renderer_default);
        transform.Find("Attack").TryGetComponent(out renderer_attack);
        transform.Find("Fin").TryGetComponent(out renderer_fin);
        this.map_size = GameObject.Find("The_Ocean").GetComponent<Map_Generator>().mapSize;

        white_albedo = new Color(1f, 1f, 1f, 50f/255f);
        red_albedo = new Color(1f, 0f, 0f, 50f/255f);
    }

    private void OnEnable()
    {
        renderer_default.color = white_albedo;
        renderer_attack.color = Color.white;
        renderer_fin.color = Color.white;
        health_current = health_max;
    }

    private void Update()
    {
        Clamp_Position();
    }

    public void Take_Damage(float damage)
    {
        health_current -= damage;
        StopCoroutine(Hit_Animation_Co());
        StartCoroutine(Hit_Animation_Co());
        if(health_current <= 0)
        {
            StartCoroutine(Die_Animation_Co());
        }
    }

    public void Clamp_Position()
    {
        clamp_x = Mathf.Clamp(transform.position.x, -(map_size / 2), map_size / 2);
        clamp_y = Mathf.Clamp(transform.position.y, -(map_size / 2), map_size / 2);
        transform.position = new Vector3(clamp_x, clamp_y, 0f);
    }

    private IEnumerator Hit_Animation_Co()
    {
        renderer_default.color = red_albedo;
        renderer_attack.color = Color.red;
        renderer_fin.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer_default.color = white_albedo;
        renderer_attack.color = Color.white;
        renderer_fin.color = Color.white;
    }

    private IEnumerator Die_Animation_Co()
    {
        int frame_count = 0;
        WaitForSeconds wait_for_1_frame = new WaitForSeconds(1f/60f);
        while(frame_count < 30)
        {
            this.transform.localScale = new Vector3(Mathf.Max(this.transform.localScale.x - 1f/30f, 0), Mathf.Max(this.transform.localScale.y - 1f/30f, 0), this.transform.localScale.z);
            frame_count++;
            yield return wait_for_1_frame;
        }
        The_Sea_Disable();
    }

    private void The_Sea_Disable()
    {
        if(gameObject.name == "The_Shark(Clone)")
        {
            gameObject.SetActive(false);
            GameObject.Find("The_Sea_Spawner").GetComponent<The_Sea_Spawner>().The_Sea_Spawner_Enqueue(gameObject);
            the_old_man.Heal(10f);
        }
        else if(gameObject.name == "The_Swordfish(Clone)")
        {
            gameObject.SetActive(false);
            GameObject.Find("The_Sea_Spawner").GetComponent<The_Sea_Spawner>().is_the_swordfish_died = true;
            GameObject.Find("The_Sea_Spawner").GetComponent<The_Sea_Spawner>().the_shark_spawn_position = gameObject.transform.position;
            the_old_man.Heal(30f);
        }
    }
}
