using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    public Point GridPos { get; private set; }

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

        //add new tiles to Dictionary so we can access each tile by x y position
        LevelManager.Instance.Tiles.Add(gridPos, this);
    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.clickedBtn != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
                Hover.Instance.Deactivate();
            }
        }
    }

    private void PlaceTower()
    {
        //place tower if mouse isn't over a button
       
        GameObject tower = (GameObject)Instantiate(GameManager.Instance.clickedBtn.TowerPrefab, transform.position, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPos.Y;
        tower.transform.SetParent(transform);

        GameManager.Instance.BuyTower();
    }
}
