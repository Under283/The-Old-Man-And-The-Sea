using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Restart : MonoBehaviour
{
    public void On_Click()
    {
        GameManager.instance.Button_Restart();
    }
}
