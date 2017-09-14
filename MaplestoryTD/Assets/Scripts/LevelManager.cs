using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        //temporary instantiation of the map level, will load through text doc later
        string[] mapData = ReadLevelText();

        int mapXSize = mapData[0].ToCharArray().Length; //x axis size
        int mapYSize = mapData.Length; //y axis size

        //world start point (top left of screen)
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));  
        for(int y = 0; y < mapYSize; y++) //y position
        {
            char[] newTiles = mapData[y].ToCharArray();

            for(int x = 0; x < mapXSize; x++) //x position
            {
                PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }
        }
    }

    //placing tile in game. 
    private void PlaceTile(string tileType, int x, int y, Vector3 WorldStart)
    { 
        //"1" = 1. parses string tileType into int, use as indexer for where tile should be placed
        int tileIndex = int.Parse(tileType); 

        // create new tile and make reference to tile in newTile variable
        GameObject newTile = Instantiate(tilePrefab[tileIndex]);

        //ues newTile variable to move tile into position
        newTile.transform.position = new Vector3(WorldStart.x + (TileSize * x), WorldStart.y - (TileSize * y), 0);
    }

    //read text doc
    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level1") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }
}
