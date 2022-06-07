using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    private static Color selectedColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        
        //throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
