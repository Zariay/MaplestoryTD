using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//static class. can use everything that is public in other classes in a class level
public static class AStar 
{
    private static Dictionary<Point, Node> nodes;

    private static void CreateNodes()
    {
        nodes = new Dictionary<Point, Node>();

        //foreach loop to look at all tiles to create a node
        foreach (TileScript tile in LevelManager.Instance.Tiles.Values)
        {
            nodes.Add(tile.GridPos, new global::Node(tile));
        }
    }
}
