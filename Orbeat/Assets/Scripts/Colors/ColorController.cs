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
            //bgcolor 2A2F34FF
            bgColor = new Color32(0x2A,0x2F,0x34,0xff),
            //playercolor EBD69DFF
            playerColor = new Color32(0xeb,0xd6,0x9d,0xff),
            //playerOrbitColor 656254FF
            playerOrbitColor = new Color32(0x65,0x62,0x54,0xff),
            //hurdlecolor F1DBA0FF
            hurdleColor = new Color32(0xf1,0xdb,0xa0,0xff),
            //glowcolor 7C755748
            glowColor = new Color32(0x7c,0x75,0x57,0x48),
            //scorecolor 7E755FFF
            scoreColor = new Color32(0x7e,0x75,0x5f,0xff),
            //explosionColor EDD79EFF 
            explosionColor = new Color32(0xed,0xd7,0x93,0xff)

            //backgroundColor = new Color(0x00, 0x00, 0x00),
            //playerOrbitColor = new Color(0xff, 0x00, 0x00),
            //targetColor = new Color(0xff, 0x83, 0x00),
            //targetOrbitColor = new Color(0xff, 0x83, 0x00),
            //playerColor = new Color(0xff, 0x83, 0x00),
            //scoreColor = new Color(0xff, 0x83, 0x00, Constants.scoreAlpha),
            //perfectTextColor = new Color(0xff, 0x83, 0x00, Constants.perfectTextAlpha),
            //shareBtnColor = new Color(0xff, 0x83, 0x00),
            //playBtnColor = new Color(0xff, 0x83, 0x00),
            //highscoreColor = new Color(0xff, 0x83, 0x00),
            ////orbit1Color = new Color(0xff, 0x00, 0x00, Constants.orbitAlpha),
            ////orbit2Color = new Color(0xff, 0x00, 0x00, Constants.orbitAlpha),
            ////orbit3Color = new Color(0xff, 0x00, 0x00, Constants.orbitAlpha),
            //orbitColor = new Color(0xff, 0x00, 0x00, Constants.orbitAlpha),
            //innerOrbitColor = new Color(0xff, 0x00, 0x00)
        };

        colorSets.Add(colorSet1);

    }

    public ColorSet GetRandomColorSet(){
        int randomColorSet = Random.Range(0, colorSets.Count);
        return colorSets[randomColorSet];
    }
	
}
