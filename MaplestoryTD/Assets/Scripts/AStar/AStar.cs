using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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
    public static Stack<Node> GetPath(Point startPoint, Point endPoint)
    {
        if(nodes == null)
        {
            CreateNodes();
        }

        //create open list to be used for A* algorithm in a hashset
        HashSet<Node> openList = new HashSet<Node>();

        //create closed list to be used for A* algorithm
        HashSet<Node> closedList = new HashSet<Node>();

        //stack that holds the final path. push and pop the nodes in the stack
        Stack<Node> finalPath = new Stack<Node>();

        //find start node and sets it as a reference
        Node currentNode = nodes[startPoint];

        //1. add the current node to the Open list
        openList.Add(currentNode);

        while(openList.Count > 0) // Step 10
        {
            //2. position of neighbours
            //X value above, beside and below 
            for (int x = -1; x <= 1; ++x)
            {
                //Y value above, beside, and below
                for (int y = -1; y <= 1; ++y)
                {
                    Point neighbourPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);

                    //Check if neighbour positions are in bounds of the map, are walkable and are not the current node
                    if (LevelManager.Instance.inBounds(neighbourPos) && LevelManager.Instance.Tiles[neighbourPos].Walkable && neighbourPos != currentNode.GridPosition)
                    {
                        //calculate gScore of tile for Node pathfinding
                        int gScore = 0;

                        if (Math.Abs(x - y) == 1)
                        {
                            gScore = 10;
                        }
                        else
                        {
                            if(!ConnectedDiagonally(currentNode, nodes[neighbourPos]))
                            {
                                continue;
                            }
                            gScore = 14;
                        }

                        //Step 3. Add neighbour to open list
                        Node neighbourNode = nodes[neighbourPos];

                        if (openList.Contains(neighbourNode))
                        {
                            if (currentNode.G + gScore < neighbourNode.G)//Step 9.4
                            {
                                neighbourNode.CalcValues(currentNode, nodes[endPoint], gScore);
                            }

                        }
                        else if (!closedList.Contains(neighbourNode))//Step 9.1
                        {
                            openList.Add(neighbourNode); //9.2
                            neighbourNode.CalcValues(currentNode, nodes[endPoint], gScore); //9.3
                        }
                    }
                }
            }

            //Step 5 and 8. remove current node from openlist and add to closed list;
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (openList.Count > 0)// Step 7 in AStar tutorial video
            {
                //lambda expression to sort list by F Value and selects first one on the list.
                currentNode = openList.OrderBy(n => n.F).First();
            }

            if(currentNode == nodes[endPoint])
            {
                while(currentNode.GridPosition != startPoint)
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.Parent;
                }
                break;
            }
        }

        //**** ONLY FOR DEBUGGING **** REMOVE LATER

        //GameObject.Find("AStarDebug").GetComponent<AStarDebug>().DebugPath(openList, closedList, finalPath);

        return finalPath;
    }

    //check if diagonal tiles next to final path are walkable or not. affects final pathing
    private static bool ConnectedDiagonally(Node currentNode, Node neighbourNode)
    {
        Point direction = neighbourNode.GridPosition - currentNode.GridPosition;

        Point first = new global::Point(currentNode.GridPosition.X + direction.X, currentNode.GridPosition.Y);

        Point second = new global::Point(currentNode.GridPosition.X, currentNode.GridPosition.Y + direction.Y);


        if(LevelManager.Instance.inBounds(first) && !LevelManager.Instance.Tiles[first].Walkable || LevelManager.Instance.inBounds(second) && !LevelManager.Instance.Tiles[second].Walkable)
        {
            return false;
        }

        return true;
    }

}
