using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class The_Sea_Spawner : MonoBehaviour
{
    private Queue<GameObject> the_shark_queue = new Queue<GameObject>();

    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject canvas_the_sea;
    [SerializeField] private GameObject approaching_swordfish;
    [SerializeField] private GameObject approaching_shark;
    
    [SerializeField] private GameObject the_swordfish;
    [SerializeField] private GameObject the_shark;
    [SerializeField] private int shark_num;

    private GameObject the_swordfish_clone;
    private GameObject the_shark_clone;

    private bool is_the_shark_spawned = false;
    public bool is_the_swordfish_died = false;
    public Vector2 the_shark_spawn_position;

    private void Awake()
    {
        the_swordfish_clone = Instantiate(the_swordfish);
        the_swordfish_clone.transform.SetParent(gameObject.transform);
        the_swordfish_clone.SetActive(false);
        for(int i = 0; i < shark_num; i++)
        {
            the_shark_clone = Instantiate(the_shark);
            the_shark_clone.transform.SetParent(gameObject.transform);
            the_shark_clone.SetActive(false);
            the_shark_queue.Enqueue(the_shark_clone);
        }
    }

    private void Start()
    {
        StartCoroutine(The_Sea_Spawner_Co());
    }

    private IEnumerator The_Sea_Spawner_Co()
    {
        Vector3 spawn_distance = new Vector3(0, 10f, 0);

        yield return new WaitForSeconds(3.0f);

        approaching_swordfish.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        approaching_swordfish.SetActive(false);

        the_swordfish_clone.SetActive(true);
        the_swordfish_clone.transform.position = GameObject.Find("The_Old_Man").transform.position + spawn_distance;
        the_swordfish_clone.transform.RotateAround(GameObject.Find("The_Old_Man").transform.position, Vector3.forward, Random.Range(0, 360));
        Create_UI_Health_Circle(the_swordfish_clone);

        yield return new WaitForSeconds(1f);
        
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (is_the_swordfish_died && !is_the_shark_spawned && the_shark_queue.Count == shark_num)
            {
                approaching_shark.SetActive(true);
                yield return new WaitForSeconds(3.5f);
                approaching_shark.SetActive(false);

                for (int i = 0; i < Random.Range(3, the_shark_queue.Count + 1); i++)
                {
                    the_shark_clone = the_shark_queue.Dequeue();
                    the_shark_clone.SetActive(true);
                    the_shark_clone.transform.position = (Vector3)the_shark_spawn_position + spawn_distance;
                    the_shark_clone.transform.RotateAround(the_shark_spawn_position, Vector3.forward, Random.Range(0, 360));
                    Create_UI_Health_Circle(the_shark_clone);
                }
                is_the_shark_spawned = true;
            }
            else if(is_the_swordfish_died && is_the_shark_spawned && the_shark_queue.Count == shark_num)
            {
                yield return new WaitForSeconds(10f);
                is_the_shark_spawned = false;
                is_the_swordfish_died = false;
                approaching_swordfish.SetActive(true);
                yield return new WaitForSeconds(3.5f);
                approaching_swordfish.SetActive(false);
                the_swordfish_clone.SetActive(true);
                the_swordfish_clone.transform.position = GameObject.Find("The_Old_Man").transform.position + spawn_distance;
                the_swordfish_clone.transform.RotateAround(GameObject.Find("The_Old_Man").transform.position, Vector3.forward, Random.Range(0, 360));
                Create_UI_Health_Circle(the_swordfish_clone);
            }
        }
    }

    public void The_Sea_Spawner_Enqueue(GameObject the_shark)
    {
        the_shark_queue.Enqueue(the_shark);
    }

    private void Create_UI_Health_Circle(GameObject entity)
    {
        GameObject slider_clone = Instantiate(circle);
        slider_clone.transform.SetParent(canvas_the_sea.transform);
        slider_clone.transform.localScale = Vector3.one;

        slider_clone.GetComponent<UI_Health_Circle>().UI_Health_Circle_Setup(entity);
    }
}
