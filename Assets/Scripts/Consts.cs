using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consts : MonoBehaviour
{
    public static Consts Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    [Header("Background Colors")]
    public Color RED_BKGR;
    public Color GREEN_BKGR;
    [Header("Text Colors")]
    public Color RED_TEXT;
    public Color BLACK_TEXT;
    public Color GOLD_TEXT;
    public Color MAGENTA_TEXT;
}
