using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class dontDestroyOnLoad : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);   
    }

}
