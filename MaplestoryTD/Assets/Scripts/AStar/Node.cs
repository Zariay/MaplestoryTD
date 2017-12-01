﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node 
{
    public Point GridPosition
    { get; private set; }
   

    public TileScript TileReference
    { get; private set; }


    public Node Parent { get; private set; }

    public int G { get; set; }
    public int F { get; set; }
    public int H { get; set; }

    public Node(TileScript tileRef)
    {
        this.TileReference = tileRef;
        this.GridPosition = tileRef.GridPos;
    }

    public void CalcValues(Node parent, Node goal, int GScore)
    {
        this.Parent = parent;
        this.G = parent.G + GScore;
        this.H = (Math.Abs(GridPosition.X - goal.GridPosition.X) + Math.Abs(goal.GridPosition.Y - GridPosition.Y)) * 10;
        this.F = G + H;
    }
}
