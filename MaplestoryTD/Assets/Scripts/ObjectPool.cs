using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monsterPrefabs;

    public GameObject GetObject(string type)
    {
        for(int i = 0; i < monsterPrefabs.Length; i++)
        {
            if(monsterPrefabs[i].name == type)
            {
                GameObject newObject = Instantiate(monsterPrefabs[i]);
                newObject.name = type;
                return newObject;
            }
        }


        return null;
    }
}
