using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MemoryPlayerController : MonoBehaviour
{
    private MemoryPattenGenerator pattern_generator;


    private void Awake()
    {
        pattern_generator = FindObjectOfType<MemoryPattenGenerator>();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
