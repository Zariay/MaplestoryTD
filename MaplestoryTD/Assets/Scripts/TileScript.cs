using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    public Point GridPos { get; private set; }

    public bool IsEmpty { get; private set; }

    private Color32 fullColor = new Color32(255, 118, 118, 255);

    private Color32 emptyColor = new Color32(96, 156, 90, 255);

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //public Color startColor;
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

        IsEmpty = true;

        //add new tiles to Dictionary so we can access each tile by x y position
        LevelManager.Instance.Tiles.Add(gridPos, this);
    }

    private void OnMouseOver()
    {
        
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.clickedBtn != null)
        {
            if(IsEmpty)
            {
                ColorTile(emptyColor);
            }
            else if(!IsEmpty)
            {
                ColorTile(fullColor);
            }

            if (Input.GetMouseButtonDown(0) && IsEmpty)
            {
                PlaceTower();
            }
        }
    }

    private void OnMouseExit()
    {
        ColorTile(Color.white);
    }

    private void PlaceTower()
    {
        //place tower if mouse isn't over a button
       
        GameObject tower = (GameObject)Instantiate(GameManager.Instance.clickedBtn.TowerPrefab, transform.position, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPos.Y;
        tower.transform.SetParent(transform);

        //check if tile is empty for monsters + tower placement
        IsEmpty = false;

        ColorTile(Color.white);

        GameManager.Instance.BuyTower();
    }

    //check if mouse over tile is occupied or empty
    private void ColorTile(Color newColor)
    {
        spriteRenderer.color = newColor;
    }
}
