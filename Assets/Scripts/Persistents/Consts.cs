using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consts : Singleton<Consts>
{
    [Header("Background Colors")]
    public Color RED_BKGR;
    public Color GREEN_BKGR;
    [Header("Text Colors")]
    public Color RED_TEXT;
    public Color BLACK_TEXT;
    public Color GOLD_TEXT;
    public Color MAGENTA_TEXT;
    [Header("Numbers...")]
    public float SCORE_TO_PASS_LEVEL;
    public float GREAT_SCORE;
    public float PERFECT_SCORE;
}
