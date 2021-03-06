﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject[] tilePrefab;

    [SerializeField]
    private CameraMovement cameraMovement;

    [SerializeField]
    private Transform map;

    private Point startPortal, endPortal;

    [SerializeField]
    private GameObject startPortPrefab;

    [SerializeField]
    private GameObject endPortPrefab;

    public Portal StartPortal { get; set; }

    public Dictionary<Point, TileScript> Tiles { get; set; }

    private Point mapSize;

    private Stack<Node> finalPath;

    public Stack<Node> Path
    {
        get
        {
            //provide every monster it's own set of nodes to follow for pathing rather than a global stack
            //prevents multiple monsters not obtaining their own path information
            if(finalPath==null)
            {
                GeneratePath();
            }
            
            return new Stack<Node>(new Stack<Node>(finalPath));
        }

    }

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
        Tiles = new Dictionary<Point, TileScript>();
        //Load map through text doc
        string[] mapData = ReadLevelText();

        mapSize = new global::Point(mapData[0].ToCharArray().Length, mapData.Length);

        int mapXSize = mapData[0].ToCharArray().Length; //x axis size
        int mapYSize = mapData.Length; //y axis size

        Vector3 maxTile = Vector3.zero;

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
        maxTile = Tiles[new Point(mapXSize - 1, mapYSize - 1)].transform.position;

        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

        SpawnPortals();
    }

    //placing tile in game. 
    private void PlaceTile(string tileType, int x, int y, Vector3 WorldStart)
    { 
        //"1" = 1. parses string tileType into int, use as indexer for where tile should be placed
        int tileIndex = int.Parse(tileType); 

        // create new tile and make reference to tile in newTile variable
        TileScript newTile = Instantiate(tilePrefab[tileIndex]).GetComponent<TileScript>();

        //uses new tile variable to change position of tile
        newTile.SetGridPosition(new Point(x, y), new Vector3(WorldStart.x + (TileSize * x), WorldStart.y - (TileSize * y), 0), map);


    }

    //read text doc
    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level1") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }

    private void SpawnPortals()
    {
        //spawns start portal
        startPortal = new Point(0, 0);
        GameObject sptmp = (GameObject)Instantiate(startPortPrefab, Tiles[startPortal].GetComponent<TileScript>().WorldPos, Quaternion.identity);
        StartPortal = sptmp.GetComponent<Portal>();
        StartPortal.name = "StartPortal";

        endPortal = new Point(17,6);
        Instantiate(endPortPrefab, Tiles[endPortal].GetComponent<TileScript>().WorldPos, Quaternion.identity);
    }

    public bool inBounds(Point Position)
    {
        return Position.X >= 0 && Position.Y >= 0 && Position.X < mapSize.X && Position.Y < mapSize.Y;
    }

    public void GeneratePath()
    {
        finalPath = AStar.GetPath(startPortal, endPortal);
    }
}
