using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //temporary prefab. Will be replaced later
    [SerializeField]
    private GameObject towerPrefab;

    public GameObject TowerPrefab
    {
        get
        {
            return towerPrefab;
        }
    }
}
