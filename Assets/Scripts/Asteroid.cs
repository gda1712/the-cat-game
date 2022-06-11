using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    private static Color selectedColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    private SpriteRenderer spriteRenderer;

    public bool isDestroyed;
    //public GameObject gameObject;
    
    // this is the position in the array x, y
    public Vector2 actualBoardCoordinatesPosition;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.isDestroyed = false;

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

    private void OnMouseDown()
    {
        //Debug.Log(this.actualBoardCoordinatesPosition);
        this.isDestroyed = true;
        Destroy(this.spriteRenderer);

        BoardManager.generateShortWay();
    }
}
