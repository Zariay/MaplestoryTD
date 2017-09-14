using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefab;

    //create property to access information;
    public float TileSize
    {
        get { return tilePrefab[1].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    // Use this for initialization
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //create level
    private void CreateLevel()
    {
        string[] mapData = new string[]
        {
            "0000" , "1111", "2222", "3333"
        };

        int mapXSize = mapData[0].ToCharArray().Length; //x axis size
        int mapYSize = mapData.Length; //y axis size

        //world start point (top left of screen)
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));  
        for(int y = 0; y < mapYSize; ++y) //y position
        {
            char[] newTiles = mapData[y].ToCharArray();

            for(int x = 0; x < mapXSize; ++x) //x position
            {
                PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }
        }
    }

    //placing tile in game. 
    private void PlaceTile(string tileType, int x, int y, Vector3 WorldStart)
    {
        int tileIndex = int.Parse(tileType); //"1" = 1. parses string data into int

        // create new tile and make reference to tile in newTile variable
        GameObject newTile = Instantiate(tilePrefab[tileIndex]);

        //ues newTile variable to move tile into position
        newTile.transform.position = new Vector3(WorldStart.x + (TileSize * x), WorldStart.y - (TileSize * y), 0);
    }
}
