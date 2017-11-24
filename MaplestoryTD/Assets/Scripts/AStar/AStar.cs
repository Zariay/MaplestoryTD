using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//static class. can use everything that is public in other classes in a class level
public static class AStar 
{
    private static Dictionary<Point, Node> nodes;

    private static void CreateNodes()
    {
        //instantiates dictionary
        nodes = new Dictionary<Point, Node>();

        //foreach loop to look at all tiles to create a node
        foreach (TileScript tile in LevelManager.Instance.Tiles.Values)
        {
            //add node to dictionary
            nodes.Add(tile.GridPos, new global::Node(tile));
        }
    }

    //find the path
    public static void GetPath(Point startPoint)
    {
        if(nodes == null)
        {
            CreateNodes();
        }

        //create open list to be used for A* algorithm in a hashset
        HashSet<Node> openList = new HashSet<Node>();

        //create closed list to be used for A* algorithm
        HashSet<Node> closedList = new HashSet<Node>();

        //find start node and sets it as a reference
        Node currentNode = nodes[startPoint];

        //add the current node to the Open list
        openList.Add(currentNode);

        //position of neighbours
        //X value above, beside and below 
        for(int x = -1; x <= 1; ++x)
        {
            //Y value above, beside, and below
            for(int y = -1; y <= 1; ++y)
            {
                Point neighbourPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);

                //Check if neighbour positions are in bounds of the map, are walkable and are not the current node
                if(LevelManager.Instance.inBounds(neighbourPos) && LevelManager.Instance.Tiles[neighbourPos].Walkable && neighbourPos != currentNode.GridPosition)
                {
                    Node neighbourNode = nodes[neighbourPos];

                    if(!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }

                    neighbourNode.CalcValues(currentNode);
                }
            }
        }

        //remove current node from openlist and add to closed list;
        openList.Remove(currentNode);
        closedList.Add(currentNode);

        //**** ONLY FOR DEBUGGING **** REMOVE LATER

        GameObject.Find("AStarDebug").GetComponent<AStarDebug>().DebugPath(openList, closedList);
    }


}
