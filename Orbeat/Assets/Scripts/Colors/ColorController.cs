using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController: MonoBehaviour {
    public List<ColorSet> colorSets;
    private int colorIndex = 0;

    public ColorSet GetRandomColorSet(){
        //int randomColorSet = Random.Range(0, colorSets.Count);
        //return colorSets[randomColorSet];
        int randomColorSet = Random.Range(11, colorSets.Count);
        return colorSets[randomColorSet];
    }
	
    public ColorSet GetIncrementalColorSet()
    {
        ColorSet cs = colorSets[colorIndex];
        colorIndex++;
        if (colorIndex == colorSets.Count)
            colorIndex = 0;
        return cs;
    }
}
