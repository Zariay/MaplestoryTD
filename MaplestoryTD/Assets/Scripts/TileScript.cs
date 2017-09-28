using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Point GridPos { get; private set; }

    //return center
    public Vector2 WorldPos
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
        }
    }

    public void SetGridPosition(Point gridPos, Vector3 worldPos, Transform parent)
    {
        this.GridPos = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);

        //add new tiles to Dictionary so we can access each tile by x y position
        LevelManager.Instance.Tiles.Add(gridPos, this);
    }
}
