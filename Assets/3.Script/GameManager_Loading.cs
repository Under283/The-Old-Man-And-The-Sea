using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_Loading : MonoBehaviour
{
    [SerializeField] private Image loading_circle;
    public static string scene_name;

    void Start()
    {
        StartCoroutine(Loading_Co());
    }

    public static void Load_Scene(string scene_name_to_load)
    {
        scene_name = scene_name_to_load;
        SceneManager.LoadScene("Scene_Loading");
    }

    private IEnumerator Loading_Co()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene_name);
        operation.allowSceneActivation = false;
        loading_circle.fillAmount = 0;
        float loading_time = 0f;

        while (!operation.isDone)
        {
            loading_time += Time.time;

            if (operation.progress < 0.9f)
            {
                loading_circle.fillAmount = Mathf.Lerp(loading_circle.fillAmount, operation.progress, loading_time);
                if (loading_circle.fillAmount >= operation.progress)
                {
                    loading_time = 0f;
                }
            }
            else
            {
                loading_circle.fillAmount = Mathf.Lerp(loading_circle.fillAmount, 1f, loading_time);
                if (loading_circle.fillAmount == 1.0f)
                {
                    yield return new WaitForSeconds(1.0f);
                    
                    operation.allowSceneActivation = true;

                    yield break;
                }
            }
        }
    }
}
