using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;

    //create property to access information;
    public float TileSize
    {
        get { return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
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
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));  
        for(int x = 0; x < 5; ++x)
        {
            for(int y = 0; y < 5; ++y)
            {
                PlaceTile(x, y, worldStart);
            }
        }
    }

    //placing tile in game. 
    private void PlaceTile(int x, int y, Vector3 WorldStart)
    {
        // create new tile 
        GameObject newTile = Instantiate(tile);

        //move new tile into position
        newTile.transform.position = new Vector3(WorldStart.x + (TileSize * x), WorldStart.y - (TileSize * y), 0);
    }
}
