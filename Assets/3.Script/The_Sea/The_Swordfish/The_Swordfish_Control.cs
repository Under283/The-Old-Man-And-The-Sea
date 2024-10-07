using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Swordfish_Control : MonoBehaviour
{
    private int pattern;
    public Vector2 target_position;

    [SerializeField] GameObject the_swordfish_attack;
    private Rigidbody2D rigidbody2D_the_sail;
    private List<Collider2D> collider_victim = new List<Collider2D>();
    private Rigidbody2D rigidbody2D_self;
    private Collider2D collider_self;
    private The_Sea the_sea_self;

    private void Awake()
    {
        TryGetComponent(out rigidbody2D_self);
        TryGetComponent(out collider_self);
        TryGetComponent(out the_sea_self);
        GameObject.Find("The_Sail").TryGetComponent(out rigidbody2D_the_sail);
    }

    private void OnEnable()
    {
        the_swordfish_attack.SetActive(false);
        transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        target_position = Camera.main.ScreenToWorldPoint(GameObject.Find("The_Old_Man").transform.position);
        pattern = 1;
        Action_Pattern();
    }

    private void Action_Pattern()
    {
        switch (pattern)
        {
            case 1:
                StartCoroutine(Action_Pattern_Chasing_Co());
                break;
            case 2:
                StartCoroutine(Action_Pattern_Lush_Multiple_Co());
                break;
            case 3:
                 StartCoroutine(Action_Pattern_Lush_Strong_Co());
                break;
        }
    }

    private void Aim_On_The_Old_Man()
    {
        target_position = GameObject.Find("The_Old_Man").transform.position;
        float angle = Mathf.Atan2(target_position.y - this.transform.position.y, target_position.x - this.transform.position.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
    private void On_Attack()
    {
        collider_victim.Clear();
        Physics2D.OverlapCollider(collider_self, new ContactFilter2D().NoFilter(), collider_victim);
        if (collider_victim != null)
            for (int i = 0; i < collider_victim.Count; i++)
                if (collider_victim[i].CompareTag("The_Old_Man"))
                {
                    collider_victim[i].transform.GetComponent<The_Old_Man>().Take_Damage(the_sea_self.attack_damage_get);
                    rigidbody2D_the_sail.AddForce((collider_victim[i].transform.position - collider_self.transform.position).normalized * 30f, ForceMode2D.Force);
                }
    }

    private IEnumerator Action_Pattern_Lush_Multiple_Co()
    {
        the_swordfish_attack.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            Aim_On_The_Old_Man();
            yield return new WaitForSeconds(0.3f);
            rigidbody2D_self.velocity = Vector2.zero;
            //rigidbody2D_self.AddForce((target_position - (Vector2)this.transform.position).normalized * 500f, ForceMode2D.Force);
            rigidbody2D_self.AddForce((this.transform.rotation * Vector3.up).normalized * 500f, ForceMode2D.Force);
            for(int j = 0; j < 5; j++)
            {
                On_Attack();
                yield return new WaitForSeconds(0.05f);
            }
        }

        yield return new WaitForSeconds(1.0f);
        the_swordfish_attack.SetActive(false);
        pattern = Random.Range(1, 4);
        Action_Pattern();
    }
    private IEnumerator Action_Pattern_Lush_Strong_Co()
    {
        the_swordfish_attack.SetActive(true);
        Aim_On_The_Old_Man();
        yield return new WaitForSeconds(0.5f);
        rigidbody2D_self.AddForce((target_position - (Vector2)this.transform.position).normalized * 1000f, ForceMode2D.Force);
        for (int j = 0; j < 20; j++)
        {
            On_Attack();
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1.0f);
        the_swordfish_attack.SetActive(false);
        pattern = Random.Range(1, 4);
        Action_Pattern();
    }
    private IEnumerator Action_Pattern_Chasing_Co()
    {
        WaitForSeconds wait_for_1_frame = new WaitForSeconds(1.0f/60.0f);
        int frame_count = 0;
        while (frame_count < 300)
        {
            Aim_On_The_Old_Man();
            if(Vector2.Distance(this.transform.position, target_position) > 5f)
            {
                rigidbody2D_self.AddForce((target_position - (Vector2)this.transform.position).normalized * 4f, ForceMode2D.Force);
            }
            else if(Vector2.Distance(this.transform.position, target_position) <= 5f)
            {
                rigidbody2D_self.AddForce(-(target_position - (Vector2)this.transform.position).normalized * 4f, ForceMode2D.Force);
            }
            
            frame_count++;
            yield return wait_for_1_frame;
        }

        pattern = Random.Range(1, 4);
        Action_Pattern();
    }
}
