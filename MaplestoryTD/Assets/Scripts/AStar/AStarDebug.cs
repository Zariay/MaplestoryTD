﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebug : MonoBehaviour
{
    private TileScript start, end;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ClickTile();
    }

    private void ClickTile()
    {
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            if(hit.collider != null)
            {
                TileScript tmp = hit.collider.GetComponent<TileScript>();

                if(tmp != null)
                {
                    if(start == null)
                    {
                        start = tmp;
                        start.Debugging = true;
                        start.SpriteRend.color = new Color32(255, 132, 0, 255);
                    }
                    else if (end == null)
                    {
                        end = tmp;
                        end.Debugging = true;
                        end.SpriteRend.color = new Color32(255, 0, 0, 255);
                    }
                }
            }
        }
    }
}
