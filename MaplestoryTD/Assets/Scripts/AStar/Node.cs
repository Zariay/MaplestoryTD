using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public Point GridPosition
    { get; private set; }
   

    public TileScript TileReference
    { get; private set; }


    public Node Parent { get; private set; }
    

    public Node(TileScript tileRef)
    {
        this.TileReference = tileRef;
        this.GridPosition = tileRef.GridPos;
    }

    public void CalcValues(Node parent)
    {
        this.Parent = parent;
    }
}
