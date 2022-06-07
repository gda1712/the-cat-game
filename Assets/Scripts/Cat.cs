using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    
    public static Cat sharedInstance;
    private static SpriteRenderer spriteRenderer;

    public static Vector3 actualPosition;
    // Start is called before the first frame update
    void Start()
    {
        
        if (sharedInstance == null)
        {
            sharedInstance = this;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
