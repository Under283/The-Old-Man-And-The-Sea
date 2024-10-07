using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass_Rotation : MonoBehaviour
{
    [SerializeField] private GameObject the_sail;
    
    void Update()
    {
        this.transform.rotation = the_sail.transform.rotation;
    }
}
