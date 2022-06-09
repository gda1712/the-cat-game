using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    
    public static Cat sharedInstance;
    private static SpriteRenderer spriteRenderer;
    public static GameObject gameObject;

    // this is the position in the screen
    public static Vector3 actualScreenPosition;

    // this is the position in the array x, y
    public static Vector2 actualBoardCoordinatesPosition;
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
