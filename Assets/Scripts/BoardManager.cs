using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;


public class BoardManager : MonoBehaviour
{
    public static BoardManager sharedInstance;
    private static Vector2 asteroidSize;
    public static List<Sprite> prefabs = new List<Sprite>();
    public GameObject currentAsteroid;
    public GameObject cat;
    public int xSize, ySize;
    
    private static GameObject[,] asteroids;

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

        asteroidSize = currentAsteroid.GetComponent<BoxCollider2D>().size;
        CreateInitialBoard();
    }
    
    
    private void CreateInitialBoard()
    {
        asteroids = new GameObject[xSize, ySize];

        float startX = this.transform.position.x - 2f;
        float oddStartX = startX + (BoardManager.asteroidSize.x / 2.0f);
        float startY = this.transform.position.y;

        for(int x = 0; x < xSize; x++)
        {
            for(int y = 0; y < ySize; y++)
            {
                GameObject newAsteroid = Instantiate(
                    currentAsteroid,
                    new Vector3( (y % 2 == 0 ? startX : oddStartX) + (BoardManager.asteroidSize.x * x),
                        startY + (BoardManager.asteroidSize.y * y),
                        0),
                    currentAsteroid.transform.rotation
                );
                newAsteroid.name = string.Format("Asteroid[{0}][{1}]", x, y);

                newAsteroid.GetComponent<Asteroid>().actualBoardCoordinatesPosition = new Vector2(x, y);
                
                asteroids[x, y] = newAsteroid;

            }
        }
        
        // Inicializate the cat
        GameObject newCat = Instantiate(
            cat,
            new Vector3( ((int)(xSize / 2.0f) % 2 != 0 ? startX : oddStartX) + (BoardManager.asteroidSize.x * (xSize / 2.0f)),
                startY + (BoardManager.asteroidSize.y * (ySize / 2.0f)),
                0),
            cat.transform.rotation
        );
        newCat.name = string.Format("Cat[{0}][{1}]", (int)(xSize / 2.0f), (int)(ySize / 2.0f));
        
        // save the cat position
        Cat.actualScreenPosition = newCat.transform.position;
        Cat.actualBoardCoordinatesPosition = new Vector2((int)(xSize / 2.0f), (int)(ySize / 2.0f));

        Cat.gameObject = newCat;
        //asteroids[(int)(xSize / 2.0f), (int)(ySize / 2.0f)] = newCat;
    }
    

    // Update is called once per frame
    void Update()
    {
        /*int movX = 0, movY = 0;
        if (Input.GetKeyDown(KeyCode.A))
            movX = -1;
        if (Input.GetKeyDown(KeyCode.D))
            movX = 1;

        if (Cat.actualBoardCoordinatesPosition.y % 2 == 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                movY = 1;
                movX = -1;
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                movY = 1;
            }
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                movY = -1;
                movX = -1;
                
            }
            
            if (Input.GetKeyDown(KeyCode.C))
            {
                movY = -1;
            }
                
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                movY = 1;
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                movY = 1;
                movX = 1;
            }
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                movY = -1;
            }
            
            if (Input.GetKeyDown(KeyCode.C))
            {
                movY = -1;
                movX = 1;
            }
        }
        
        
        

        //if (movX != 0 || movY != 0)
        //{
            if (!BoardManager.asteroids[(int)Cat.actualBoardCoordinatesPosition.x + movX, (int)Cat.actualBoardCoordinatesPosition.y + movY]
                    .GetComponent<Asteroid>().isDestroyed)
            {
                // Set the new position
                Cat.actualBoardCoordinatesPosition.x = Cat.actualBoardCoordinatesPosition.x + movX;
                Cat.actualBoardCoordinatesPosition.y = Cat.actualBoardCoordinatesPosition.y + movY;

                Cat.actualScreenPosition = BoardManager.asteroids[(int)Cat.actualBoardCoordinatesPosition.x + movX,
                        (int)Cat.actualBoardCoordinatesPosition.y + movY]
                    .transform.position;
                // Move the sprite
            
                //Debug.Log(Cat.actualBoardCoordinatesPosition);

                Cat.gameObject.transform.position = new Vector3(Cat.actualScreenPosition.x,
                    Cat.actualScreenPosition.y,
                    0);
            }
        //}*/
    }


    public static void generateShortWay()
    {
        Cell origin = new Cell((int) Cat.actualBoardCoordinatesPosition.x, (int) Cat.actualBoardCoordinatesPosition.y);

        // create the map
        int[] map = new int[BoardManager.asteroids.GetLength(0) * BoardManager.asteroids.GetLength(1)];
        
        // k is the position in the unidimentional array
        int k = 0;
        
        for (int i = 0; i < BoardManager.asteroids.GetLength(0); i++)
        {
            for (int j = 0; j < BoardManager.asteroids.GetLength(1); j++)
            {
                if (BoardManager.asteroids[i, j].GetComponent<Asteroid>().isDestroyed)
                    map[k] = 1;
                
                else
                    map[k] = 0;

                k = k + 1;
            };
        }

        List<Cell> shortWay = null;

        // get the short way
        for (int i = 0; i < BoardManager.asteroids.GetLength(0); i++)
        {
            Cell dest = new Cell(i, 0);
            
            List<Cell> auxShortWay = Globals.getShortestPath(origin, dest, map, BoardManager.asteroids.GetLength(0), BoardManager.asteroids.GetLength(1));

            if(auxShortWay.Count <= 1)
                continue;
            
            if (!checkValidMovement(auxShortWay[1]))
            {
                continue;
            }
            
            shortWay = minorList(shortWay, auxShortWay);
        }
        
        
        for (int i = 0; i < BoardManager.asteroids.GetLength(0); i++)
        {
            Cell dest = new Cell(i, BoardManager.asteroids.GetLength(1) - 1);
            
            List<Cell> auxShortWay = Globals.getShortestPath(origin, dest, map, BoardManager.asteroids.GetLength(0), BoardManager.asteroids.GetLength(1));
            
            if(auxShortWay.Count <= 1)
                continue;
            
            if (!checkValidMovement(auxShortWay[1]))
            {
                continue;
            }
            
            shortWay = minorList(shortWay, auxShortWay);
        }

        for (int j = 0; j< BoardManager.asteroids.GetLength(1); j++)
        {
            Cell dest = new Cell(0, j);
            
            List<Cell> auxShortWay = Globals.getShortestPath(origin, dest, map, BoardManager.asteroids.GetLength(0), BoardManager.asteroids.GetLength(1));
            
            if(auxShortWay.Count <= 1)
                continue;
            
            if (!checkValidMovement(auxShortWay[1]))
            {
                continue;
            }
            
            shortWay = minorList(shortWay, auxShortWay);
        }
        
        for (int j = 0; j< BoardManager.asteroids.GetLength(1); j++)
        {
            Cell dest = new Cell(BoardManager.asteroids.GetLength(0) - 1, j);
            
            List<Cell> auxShortWay =  Globals.getShortestPath(origin, dest, map, BoardManager.asteroids.GetLength(0), BoardManager.asteroids.GetLength(1));
            
            if(auxShortWay.Count <= 1)
                continue;
            
            if (!checkValidMovement(auxShortWay[1]))
            {
                continue;
            }

            shortWay = minorList(shortWay, auxShortWay);
        }
        
        
        // move the cat
        moveCat(shortWay[1]);
    }
    
    
    public static List<Cell> minorList(List<Cell> listOne, List<Cell> listTwo)
    {
        if (listOne == null)
            return listTwo;

        if (listTwo.Count < listOne.Count)
            return listTwo;

        return listOne;
    }


    public static bool checkValidMovement(Cell next)
    {
        /* This ethdo check if the next possible movement of the cat is valid */

        if (BoardManager.asteroids[next.getx(), next.gety()].GetComponent<Asteroid>().isDestroyed)
        {
            return false;
        }
        
        if (Cat.actualBoardCoordinatesPosition.y % 2 == 0)
        {
            if (next.getx() == Cat.actualBoardCoordinatesPosition.x + 1 &&
                next.gety() == Cat.actualBoardCoordinatesPosition.y + 1)
                return false;

            if (next.getx() == Cat.actualBoardCoordinatesPosition.x + 1 &&
                next.gety() == Cat.actualBoardCoordinatesPosition.y - 1)
                return false;
        }
        else
        {
            if (next.getx() == Cat.actualBoardCoordinatesPosition.x - 1 &&
                next.gety() == Cat.actualBoardCoordinatesPosition.y - 1)
                return false;

            if (next.getx() == Cat.actualBoardCoordinatesPosition.x - 1 &&
                next.gety() == Cat.actualBoardCoordinatesPosition.y + 1)
                return false;
        }

        return true;

    }


    public static void moveCat(Cell nextMovement)
    {
        /* This function move the cat */
        // Set the new position
        Cat.actualBoardCoordinatesPosition.x = nextMovement.getx();
        Cat.actualBoardCoordinatesPosition.y = nextMovement.gety();

        Cat.actualScreenPosition = BoardManager.asteroids[ nextMovement.getx(), nextMovement.gety()]
            .transform.position;
        // Move the sprite

        Cat.gameObject.transform.position = new Vector3(Cat.actualScreenPosition.x,
            Cat.actualScreenPosition.y,
            0);
    }
}
