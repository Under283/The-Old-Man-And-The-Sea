using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    [SerializeField] private GameObject canvas_game_manager;
    [SerializeField] private GameObject the_old_man;

    public void The_Sail_Over()
    {
        canvas_game_manager.transform.Find("Background").gameObject.SetActive(true);
        canvas_game_manager.transform.Find("Text_Gameover").gameObject.SetActive(true);
    }

    public void Menu_ESC_Open()
    {
        Time.timeScale = 0f;
        canvas_game_manager.transform.Find("Menu_Pause").gameObject.SetActive(true);
    }
    public void Button_Title()
    {
        Time.timeScale = 1f;
        GameManager_Loading.Load_Scene("Scene_Main");
    }
    public void Button_Restart()
    {
        Time.timeScale = 1f;
        GameManager_Loading.Load_Scene("Scene_Sail");
    }
    public void Button_Continue()
    {
        Time.timeScale = 1f;
        canvas_game_manager.transform.Find("Menu_Pause").gameObject.SetActive(false);
    }

    private void Update()
    {
        if(the_old_man.GetComponent<The_Old_Man>().is_died && Input.GetKeyDown(KeyCode.Space))
        {
            GameManager_Loading.Load_Scene("Scene_Sail");
        }
    }
}
