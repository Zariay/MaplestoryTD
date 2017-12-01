using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTile : MonoBehaviour
{
    [SerializeField]
    private Text FText;

    [SerializeField]
    private Text GText;

    [SerializeField]
    private Text HText;

    public Text F
    {
        get
        {
            FText.gameObject.SetActive(true);
            return FText;
        }

        set
        {
            FText = value;
        }
    }

    public Text G
    {
        get
        {
            GText.gameObject.SetActive(true);
            return GText;
        }

        set
        {
            GText = value;
        }
    }

    public Text H
    {
        get
        {
            HText.gameObject.SetActive(true);
            return HText;
        }

        set
        {
            HText = value;
        }
    }
}
