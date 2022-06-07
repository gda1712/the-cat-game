using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    public static BoardManager sharedInstance;
    public List<Sprite> prefabs = new List<Sprite>();
    public GameObject currentAsteroid;
    public GameObject cat;
    public int xSize, ySize;
    
    private GameObject[,] asteroids;

    // Start is called before the first frame update
    void Start()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Vector2 offset = currentAsteroid.GetComponent<BoxCollider2D>().size;
        CreateInitialBoard(offset);
    }
    
    
    private void CreateInitialBoard(Vector2 offset)
    {
        asteroids = new GameObject[xSize, ySize];

        float startX = this.transform.position.x - 2f;
        float oddStartX = startX + (offset.x / 2.0f);
        float startY = this.transform.position.y;

        for(int x = 0; x < xSize; x++)
        {
            for(int y = 0; y < ySize; y++)
            {
                GameObject newAsteroid = Instantiate(
                    currentAsteroid,
                    new Vector3( (y % 2 == 0 ? startX : oddStartX) + (offset.x * x),
                        startY + (offset.y * y),
                        0),
                    currentAsteroid.transform.rotation
                );
                newAsteroid.name = string.Format("Asteroid[{0}][{1}]", x, y);
                asteroids[x, y] = newAsteroid;

            }
        }
        
        // Inicializate the cat
        GameObject newCat = Instantiate(
            cat,
            new Vector3( ((int)(ySize / 2.0f) % 2 == 0 ? startX : oddStartX) + (offset.x * (xSize / 2.0f)),
                startY + (offset.y * (ySize / 2.0f)),
                0),
            cat.transform.rotation
        );
        newCat.name = string.Format("Cat[{0}][{1}]", (int)(xSize / 2.0f), (int)(ySize / 2.0f));
        
        // save the cat position
        Cat.actualPosition = newCat.transform.position;
        //asteroids[(int)(xSize / 2.0f), (int)(ySize / 2.0f)] = newCat;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
