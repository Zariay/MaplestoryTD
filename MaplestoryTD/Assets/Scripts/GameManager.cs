using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public TowerButton clickedBtn { get; set; }

    public ObjectPool objectPool { get; set; }

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

    private void Awake()
    {
        objectPool = GetComponent<ObjectPool>();
    }


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

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }

    public IEnumerator SpawnWave()
    {
        //CoRoutine to make mobs spawn over time

        int monsterIdx = Random.Range(0, 2);

        string type = string.Empty;

        switch (monsterIdx)
        {
            case 0:
                type = "BlueSnail";
                break;
            case 1:
                type = "RedSnail";
                break;
            default:
                type = string.Empty;
                break;
        }

        Monster monster = objectPool.GetObject(type).GetComponent<Monster>();
        monster.Spawn();

        yield return new WaitForSeconds(2.5f);
    }
}
