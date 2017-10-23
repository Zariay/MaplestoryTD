using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TowerButton clickedBtn { get; private set; }

    public void PickTower(TowerButton towerBtn)
    {
        this.clickedBtn = towerBtn;
        Hover.Instance.Activate(towerBtn.Sprite);
    }

    public void BuyTower()
    {
        clickedBtn = null;
    }
}
