using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController: MonoBehaviour {
    public List<ColorSet> colorSets;
    private int colorIndex = 0;
    //public void Initialize(){
    //    //colorSets = new List<ColorSet>();
    //    //AddColor();
    //}

    //private void AddColor(){
    //    ColorSet colorSet1 = new ColorSet
    //    {
    //        //bgcolor 2A2F34FF
    //        bgColor = new Color32(0x2A,0x2F,0x34,0xff),
    //        //playercolor EBD69DFF
    //        playerColor = new Color32(0xeb,0xd6,0x9d,0xff),
    //        //playerOrbitColor 656254FF
    //        playerOrbitColor = new Color32(0x65,0x62,0x54,0xff),
    //        playerOrbitGlowColor = new Color32(0xf1, 0xdb, 0xa0, 0x00),
    //        //hurdlecolor F1DBA0FF
    //        hurdleColor = new Color32(0xf1,0xdb,0xa0,0xff),
    //        //glowcolor 7C755748
    //        glowColor = new Color32(0x7c,0x75,0x57,0x48),
    //        //scorecolor 7E755FFF
    //        scoreColor = new Color32(0x7e,0x75,0x5f,0xff),
    //        //explosionColor EDD79EFF 
    //        explosionColor = new Color32(0xed,0xd7,0x93,0xff)

    //        //backgroundColor = new Color(0x00, 0x00, 0x00),
    //        //playerOrbitColor = new Color(0xff, 0x00, 0x00),
    //        //targetColor = new Color(0xff, 0x83, 0x00),
    //        //targetOrbitColor = new Color(0xff, 0x83, 0x00),
    //        //playerColor = new Color(0xff, 0x83, 0x00),
    //        //scoreColor = new Color(0xff, 0x83, 0x00, Constants.scoreAlpha),
    //        //perfectTextColor = new Color(0xff, 0x83, 0x00, Constants.perfectTextAlpha),
    //        //shareBtnColor = new Color(0xff, 0x83, 0x00),
    //        //playBtnColor = new Color(0xff, 0x83, 0x00),
    //        //highscoreColor = new Color(0xff, 0x83, 0x00),
    //        ////orbit1Color = new Color(0xff, 0x00, 0x00, Constants.orbitAlpha),
    //        ////orbit2Color = new Color(0xff, 0x00, 0x00, Constants.orbitAlpha),
    //        ////orbit3Color = new Color(0xff, 0x00, 0x00, Constants.orbitAlpha),
    //        //orbitColor = new Color(0xff, 0x00, 0x00, Constants.orbitAlpha),
    //        //innerOrbitColor = new Color(0xff, 0x00, 0x00)
    //    };

    //    ColorSet colorSet2 = new ColorSet
    //    {
    //        //bgcolor 3A 12 13 FF
    //        bgColor = new Color32(0x3A, 0x12, 0x13, 0xFF),

    //        //playercolor DC 6F 36 FF
    //        playerColor = new Color32(0xDC, 0x6F, 0x36, 0xFF),

    //        //playerOrbitColor AE 3A 2A FF
    //        playerOrbitColor = new Color32(0xAE, 0x3A, 0x2A, 0xFF),
    //        playerOrbitGlowColor = new Color32(0xDC, 0x6F, 0x36, 0x00),

    //        //hurdlecolor DC 6F 36 FF
    //        hurdleColor = new Color32(0xDC, 0x6F, 0x36, 0xFF),

    //        //glowcolor AF 2F 29 FF
    //        glowColor = new Color32(0xAF, 0x2F, 0x29, 0x48),

    //        //scorecolor DC 54 32 FF
    //        scoreColor = new Color32(0xDC, 0x54, 0x32, 0xFF),

    //        //explosioncolor DC 6F 36 FF
    //        explosionColor = new Color32(0xDC, 0x6F, 0x36, 0xFF)

    //    };

    //    ColorSet colorSet3 = new ColorSet
    //    {
    //        //bgcolor 061A24FF
    //        bgColor = new Color32(0x06, 0x1A, 0x24, 0xff),

    //        //playercolor 64BD58FF
    //        playerColor = new Color32(0x64, 0xBD, 0x58, 0xff),

    //        //playerOrbitColor 2C6537FF
    //        playerOrbitColor = new Color32(0x2C, 0x65, 0x37, 0xff),
    //        playerOrbitGlowColor = new Color32(0x4E, 0xB6, 0x5A, 0x00),

    //        //hurdlecolor 4EB65AFF
    //        hurdleColor = new Color32(0x4E, 0xB6, 0x5A, 0xff),

    //        //glowcolor 27 53 2E FF
    //        glowColor = new Color32(0x27, 0x53, 0x2E, 0x48),

    //        //scorecolor 38 89 49 FF
    //        scoreColor = new Color32(0x38, 0x89, 0x49, 0xFF),

    //        //explosioncolor 64 BD 58 FF
    //        explosionColor = new Color32(0x64, 0xBD, 0x58, 0xFF)

    //    };

    //    ColorSet colorSet4 = new ColorSet
    //    {
    //        //bgcolor 47 3C 64 FF  
    //        //332752FF
    //        bgColor = new Color32(0x33, 0x27, 0x52, 0xff),

    //        //playercolor 7B CB A9 FF
    //        playerColor = new Color32(0x7B, 0xCB, 0xA9, 0xFF),

    //        //playerOrbitColor 52 8D A6 FF
    //        playerOrbitColor = new Color32(0x52, 0x8D, 0xA6, 0xFF),
    //        playerOrbitGlowColor = new Color32(0x7B, 0xCB, 0xA9, 0x00),

    //        //hurdlecolor 7B CB A9 FF
    //        hurdleColor = new Color32(0x7B, 0xCB, 0xA9, 0xFF),

    //        //glowcolor 53 78 A7 FF 385C9448
    //        glowColor = new Color32(0x38, 0x5C, 0x94, 0x48),

    //        //scorecolor 53 A2 B1 FF
    //        scoreColor = new Color32(0x53, 0xA2, 0xB1, 0xFF),

    //        //explosioncolor 7B CB A9 FF
    //        explosionColor = new Color32(0x7B, 0xCB, 0xA9, 0xFF),

    //    };

    //    colorSets.Add(colorSet1);
    //    colorSets.Add(colorSet2);
    //    colorSets.Add(colorSet3);
    //    colorSets.Add(colorSet4);


    //}

    public ColorSet GetRandomColorSet(){
        int randomColorSet = Random.Range(0, colorSets.Count);
        return colorSets[randomColorSet];
    }
	
    public ColorSet GetIncrementalColorSet()
    {
        ColorSet cs = colorSets[colorIndex];
        print("Color Index " + colorIndex);
        colorIndex++;
        if (colorIndex == colorSets.Count)
            colorIndex = 0;
        return cs;
    }
}
