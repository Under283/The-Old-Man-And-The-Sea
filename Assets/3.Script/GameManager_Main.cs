using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_Main : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager_Loading.Load_Scene("Scene_Sail");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
