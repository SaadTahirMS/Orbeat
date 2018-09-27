using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController: MonoBehaviour {
    public List<ColorSetGroup> colorSetGroups;
    private int colorIndex = 0;

    //public ColorSet GetRandomColorSet(){
    //    int randomColorSet = Random.Range(0, colorSets.Count);
    //    return colorSets[randomColorSet];
    //    //int randomColorSet = Random.Range(11, colorSets.Count);
    //    //return colorSets[randomColorSet];
    //}
	
    //public ColorSet GetIncrementalColorSet()
    //{
    //    ColorSet cs = colorSets[colorIndex];
    //    colorIndex++;
    //    if (colorIndex == colorSets.Count)
    //        colorIndex = 0;
    //    return cs;
    //}

    public ColorSet GetGroupColors(int index){
        if (index > colorSetGroups.Count){
            index = Random.Range(0, colorSetGroups.Count);
        }
        int randomColorSet = Random.Range(0, colorSetGroups[index].colorSets.Count);
        ColorSet colorSet = colorSetGroups[index].colorSets[randomColorSet];
        return colorSet;
    }
}
