using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorSetGroup {

    public List<ColorSet> colorSets;

    //public Color32 bgColor;
    //public Color32 playerColor;
    //public Color32 playerOrbitColor;
    //public Color32 hurdleColor;
    //public Color32 scoreColor;
    //public Color32 explosionColor;

}

[System.Serializable]
public class ColorSet
{
    public Color32 bgColor;
    public Color32 playerColor;
    public Color32 playerOrbitColor;
    public Color32 hurdleColor;
    public Color32 scoreColor;
    public Color32 explosionColor;
}
