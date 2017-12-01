using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public TowerButton clickedBtn { get; set; }

    //currency to purchase tower
    public int Meso
    {
        get
        {
            return meso;
        }

        set
        {
            this.meso = value;
            this.currencyText.text = "<color=lime>$</color>" + value.ToString();
        }
    }
    private int meso;
    [SerializeField]
    private Text currencyText;

    void Start()
    {
        Meso = 20;
    }

    void Update()
    {
        HandleEscapeBtn();
        currencyText.text = "<color=lime>$</color>" + meso.ToString();
    }

    public void PickTower(TowerButton towerBtn)
    {
        if(Meso >= towerBtn.Price)
        {
            //store clicked button
            this.clickedBtn = towerBtn;
            //activates hover icon
            Hover.Instance.Activate(towerBtn.Sprite);
        }
    }

    public void BuyTower()
    {
        if(Meso >= clickedBtn.Price)
        {
            Meso -= clickedBtn.Price;

            Hover.Instance.Deactivate();
        }
    }

    //handle the various escape button functions
    private void HandleEscapeBtn()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }
}
