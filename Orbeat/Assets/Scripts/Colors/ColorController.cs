using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController {
    private List<ColorSet> colorSets;

    public void Initialize(){
        colorSets = new List<ColorSet>();
        AddColor();
    }

    private void AddColor(){
        ColorSet colorSet1 = new ColorSet
        {
            gameColor = new Color(0x00, 0x00, 0x00, 0x00),
            bgColor = new Color(0x00,0x00,0x00,0x00)
        };

        colorSets.Add(colorSet1);

    }

    public ColorSet GetRandomColorSet(){
        int randomColorSet = Random.Range(0, colorSets.Count);
        return colorSets[randomColorSet];
    }
	
}
