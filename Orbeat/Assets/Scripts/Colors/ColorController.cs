using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController {
    private List<ColorSet> colorSets;

    //private Color green = new Color(0xaa, 0xff, 0x00, 1);
    //private Color orange = new Color(0xff, 0xaa, 0x00, 1);
    //private Color pink = new Color(0xff, 0x00, 0xaa, 1);
    //private Color purple = new Color(0xaa, 0x00, 0xff, 1);
    //private Color blue = new Color(0x00, 0xaa, 0xff, 1);
    private Color perfectHitYellow = new Color(1f, 0.92f, 0.016f, Constants.perfectTextAlpha);
    private Color scoreYellow = new Color(1f, 0.92f, 0.016f, Constants.scoreAlpha);

    public void Initialize(){
        colorSets = new List<ColorSet>();
        AddColor();
    }

    private void AddColor(){
        //        ColorSet colorSet1 = new ColorSet
        //        {
        //            backgroundColor = new Color(0x00, 0x00, 0x00),
        //            playerOrbitColor = new Color(0xff, 0x00, 0x00),
        //            targetColor = new Color(0xff, 0x83, 0x00),
        //            hurdle1Color = new Color(0x06, 0x79, 0x9f),
        //            hurdle2Color = new Color(0x04, 0x63, 0x82),
        //            hurdle3Color = new Color(0x3f, 0xa2, 0xc3),
        //            hurdle4Color = new Color(0x03, 0x51, 0x6b),
        //            targetOrbitColor = new Color(0xff,0x83,0x00),
        //            playerColor = new Color(0xff, 0x83, 0x00),
        //            playerArrowColor = new Color(0xff, 0x83, 0x00),
        //            scoreColor = new Color(0xff, 0x83, 0x00, Constants.scoreAlpha),
        //            perfectTextColor = new Color(0xff, 0x83, 0x00,Constants.perfectTextAlpha),
        //            shareBtnColor = new Color(0xff, 0x83, 0x00),
        //            playBtnColor = new Color(0xff, 0x83, 0x00),
        //            highscoreColor = new Color(0xff, 0x83, 0x00),
        //            orbit1Color = new Color(0xff, 0x00, 0x00, Constants.orbitAlpha),
        //            orbit2Color = new Color(0xff, 0x00, 0x00, Constants.orbitAlpha),
        //            orbit3Color = new Color(0xff, 0x00, 0x00, Constants.orbitAlpha),
        //            innerOrbitColor = new Color(0xff, 0x00, 0x00)
        //        };
        //        ColorSet colorSet2 = new ColorSet
        //        {
        //            backgroundColor = new Color(0x00, 0x00, 0x00),
        //            playerOrbitColor = new Color(0xff, 0xd0, 0x01),
        //            targetColor = new Color(0xd0, 0xff, 0x00),
        //            hurdle1Color = new Color(0xff, 0x00, 0xf4),
        //            hurdle2Color = new Color(0xda, 0x00, 0xd0),
        //            hurdle3Color = new Color(0xff, 0x55, 0xf7),
        //            hurdle4Color = new Color(0xff, 0x2f, 0xf6),
        //            targetOrbitColor = new Color(0xd0, 0xff, 0x00),
        //            playerColor = new Color(0xd0, 0xff, 0x00),
        //            playerArrowColor = new Color(0xd0, 0xff, 0x00),
        //scoreColor = new Color(0xd0, 0xff, 0x00, Constants.scoreAlpha),
        //            perfectTextColor = new Color(0xd0, 0xff, 0x00,Constants.perfectTextAlpha),
        //            shareBtnColor = new Color(0xd0, 0xff, 0x00),
        //            playBtnColor = new Color(0xd0, 0xff, 0x00),
        //            highscoreColor = new Color(0xd0, 0xff, 0x00),
        //            orbit1Color = new Color(0xff, 0xd0, 0x01, Constants.orbitAlpha),
        //            orbit2Color = new Color(0xff, 0xd0, 0x01, Constants.orbitAlpha),
        //            orbit3Color = new Color(0xff, 0xd0, 0x01, Constants.orbitAlpha),
        //            innerOrbitColor = new Color(0xff, 0xd0, 0x01)
        //        };
        //        ColorSet colorSet3 = new ColorSet
        //        {
        //            backgroundColor = new Color(0x00, 0x00, 0x00),
        //            playerOrbitColor = new Color(0xcf, 0xff, 0x00),
        //            targetColor = new Color(0xcf, 0xff, 0x00),
        //            hurdle1Color = new Color(0xff, 0x00, 0xe9),
        //            hurdle2Color = new Color(0xdb, 0x00, 0xc9),
        //            hurdle3Color = new Color(0xff, 0x55, 0xf1),
        //            hurdle4Color = new Color(0xff, 0x2f, 0xed),
        //            playerColor = new Color(0xcf, 0xff, 0x00),
        //            targetOrbitColor = new Color(0xcf, 0xff, 0x00),
        //            playerArrowColor = new Color(0xcf, 0xff, 0x00),
        //            scoreColor = new Color(0xcf, 0xff, 0x00, Constants.scoreAlpha),
        //            perfectTextColor = new Color(0xcf, 0xff, 0x00, Constants.perfectTextAlpha),
        //            shareBtnColor = new Color(0xcf, 0xff, 0x00),
        //            playBtnColor = new Color(0xcf, 0xff, 0x00),
        //            highscoreColor = new Color(0xcf, 0xff, 0x00),
        //            orbit1Color = new Color(0xcf, 0xff, 0x00, Constants.orbitAlpha),
        //            orbit2Color = new Color(0xcf, 0xff, 0x00, Constants.orbitAlpha),
        //            orbit3Color = new Color(0xcf, 0xff, 0x00, Constants.orbitAlpha),
        //            innerOrbitColor = new Color(0xcf, 0xff, 0x00)
        //        };
        //        ColorSet colorSet4 = new ColorSet
        //        {
        //            backgroundColor = new Color(0x00, 0x00, 0x00),
        //            playerOrbitColor = new Color(0x5a, 0xff, 0x00),
        //            targetColor = new Color(0x74, 0xed, 0x9a),
        //            hurdle1Color = new Color(0xff, 0x8f, 0x7d),
        //            hurdle2Color = new Color(0xcf, 0x48, 0x33),
        //            hurdle3Color = new Color(0xe5, 0x68, 0x54),
        //            hurdle4Color = new Color(0xff, 0xa5, 0x97),
        //            playerColor = new Color(0x74, 0xed, 0x9a),
        //            targetOrbitColor = new Color(0x74, 0xed, 0x9a),
        //            playerArrowColor = new Color(0x74, 0xed, 0x9a),
        //            scoreColor = new Color(0x74, 0xed, 0x9a, Constants.scoreAlpha),
        //            perfectTextColor = new Color(0x74, 0xed, 0x9a, Constants.perfectTextAlpha),
        //            shareBtnColor = new Color(0x74, 0xed, 0x9a),
        //            playBtnColor = new Color(0x74, 0xed, 0x9a),
        //            highscoreColor = new Color(0x74, 0xed, 0x9a),
        //            orbit1Color = new Color(0x5a, 0xff, 0x00, Constants.orbitAlpha),
        //            orbit2Color = new Color(0x5a, 0xff, 0x00, Constants.orbitAlpha),
        //            orbit3Color = new Color(0x5a, 0xff, 0x00, Constants.orbitAlpha),
        //            innerOrbitColor = new Color(0x5a, 0xff, 0x00)
        //        };
        //        ColorSet colorSet5 = new ColorSet
        //        {
        //            backgroundColor = new Color(0x00, 0x00, 0x00),
        //            playerOrbitColor = new Color(0x00, 0xff, 0x9c),
        //            targetColor = new Color(0x00, 0xff, 0x9c),
        //            hurdle1Color = new Color(0xff, 0x4f, 0x00),
        //            hurdle2Color = new Color(0xac, 0x36, 0x00),
        //            hurdle3Color = new Color(0xd1, 0x41, 0x00),
        //            hurdle4Color = new Color(0xff, 0x70, 0x30),
        //            playerColor = new Color(0x00, 0xff, 0x9c),
        //            targetOrbitColor = new Color(0x00, 0xff, 0x9c),
        //            playerArrowColor = new Color(0x00, 0xff, 0x9c),
        //            scoreColor = new Color(0x00, 0xff, 0x9c, Constants.scoreAlpha),
        //            perfectTextColor = new Color(0x00, 0xff, 0x9c, Constants.perfectTextAlpha),
        //            shareBtnColor = new Color(0x00, 0xff, 0x9c),
        //            playBtnColor = new Color(0x00, 0xff, 0x9c),
        //            highscoreColor = new Color(0x00, 0xff, 0x9c),
        //            orbit1Color = new Color(0x00, 0xff, 0x9c, Constants.orbitAlpha),
        //            orbit2Color = new Color(0x00, 0xff, 0x9c, Constants.orbitAlpha),
        //            orbit3Color = new Color(0x00, 0xff, 0x9c, Constants.orbitAlpha),
        //            innerOrbitColor = new Color(0x00, 0xff, 0x9c)
        //        };
        //        ColorSet colorSet6 = new ColorSet
        //        {
        //            backgroundColor = new Color(0x00, 0x00, 0x00),
        //            playerOrbitColor = new Color(0x00, 0x9d, 0xff),
        //            targetColor = new Color(0x00, 0x9d, 0xff),
        //            hurdle1Color = new Color(0xff, 0x8e, 0x00),
        //            hurdle2Color = new Color(0xac, 0x60, 0x00),
        //            hurdle3Color = new Color(0xd1, 0x75, 0x00),
        //            hurdle4Color = new Color(0xff, 0xa3, 0x30),
        //            playerColor = new Color(0x00, 0x9d, 0xff),
        //            targetOrbitColor = new Color(0x00, 0x9d, 0xff),
        //            playerArrowColor = new Color(0x00, 0x9d, 0xff),
        //            scoreColor = new Color(0x00, 0x9d, 0xff, Constants.scoreAlpha),
        //            perfectTextColor = new Color(0x00, 0x9d, 0xff, Constants.perfectTextAlpha),
        //            shareBtnColor = new Color(0x00, 0x9d, 0xff),
        //            playBtnColor = new Color(0x00, 0x9d, 0xff),
        //            highscoreColor = new Color(0x00, 0x9d, 0xff),
        //            orbit1Color = new Color(0x00, 0x9d, 0xff, Constants.orbitAlpha),
        //            orbit2Color = new Color(0x00, 0x9d, 0xff, Constants.orbitAlpha),
        //            orbit3Color = new Color(0x00, 0x9d, 0xff, Constants.orbitAlpha),
        //            innerOrbitColor = new Color(0x00, 0x9d, 0xff)
        //        };
        //        ColorSet colorSet7 = new ColorSet
        //        {
        //            backgroundColor = new Color(0x00, 0x00, 0x00),
        //            playerOrbitColor = new Color(0x89, 0x00, 0xf9),
        //            targetColor = new Color(0x89, 0x00, 0xf9),
        //            hurdle1Color = new Color(0xff, 0xf5, 0x00),
        //            hurdle2Color = new Color(0xac, 0xa5, 0x00),
        //            hurdle3Color = new Color(0xd1, 0xc9, 0x00),
        //            hurdle4Color = new Color(0xff, 0xf7, 0x30),
        //            playerColor = new Color(0x89, 0x00, 0xf9),
        //            targetOrbitColor = new Color(0x89, 0x00, 0xf9),
        //            playerArrowColor = new Color(0x89, 0x00, 0xf9),
        //            scoreColor = new Color(0x89, 0x00, 0xf9, Constants.scoreAlpha),
        //            perfectTextColor = new Color(0x89, 0x00, 0xf9, Constants.perfectTextAlpha),
        //            shareBtnColor =new Color(0x89, 0x00, 0xf9),
        //            playBtnColor =new Color(0x89, 0x00, 0xf9),
        //            highscoreColor = new Color(0x89, 0x00, 0xf9),
        //            orbit1Color = new Color(0x89, 0x00, 0xf9, Constants.orbitAlpha),
        //            orbit2Color = new Color(0x89, 0x00, 0xf9, Constants.orbitAlpha),
        //            orbit3Color = new Color(0x89, 0x00, 0xf9, Constants.orbitAlpha),
        //            innerOrbitColor = new Color(0x89, 0x00, 0xf9)
        //        };
        //        ColorSet colorSet8 = new ColorSet
        //        {
        //            backgroundColor = new Color(0x00, 0x00, 0x00),
        //            playerOrbitColor = new Color(0x01, 0x0e, 0xff),
        //            targetColor = new Color(0x01, 0x0e, 0xff),
        //            hurdle1Color = new Color(0xff, 0xbc, 0x00),
        //            hurdle2Color = new Color(0xff, 0xbc, 0x00),
        //            hurdle3Color = new Color(0xff, 0xbc, 0x00),
        //            hurdle4Color = new Color(0xff, 0xbc, 0x00),
        //            playerColor = new Color(0x01, 0x0e, 0xff),
        //            targetOrbitColor = new Color(0x01, 0x0e, 0xff),
        //            playerArrowColor = new Color(0x01, 0x0e, 0xff),
        //            scoreColor = new Color(0x01, 0x0e, 0xff, Constants.scoreAlpha),
        //            perfectTextColor = new Color(0x01, 0x0e, 0xff, Constants.perfectTextAlpha),
        //            shareBtnColor = new Color(0x01, 0x0e, 0xff),
        //            playBtnColor = new Color(0x01, 0x0e, 0xff),
        //            highscoreColor = new Color(0x01, 0x0e, 0xff),
        //            orbit1Color = new Color(0x01, 0x0e, 0xff, Constants.orbitAlpha),
        //            orbit2Color = new Color(0x01, 0x0e, 0xff, Constants.orbitAlpha),
        //            orbit3Color = new Color(0x01, 0x0e, 0xff, Constants.orbitAlpha),
        //            innerOrbitColor = new Color(0x01, 0x0e, 0xff)
        //        };
        //        ColorSet colorSet9 = new ColorSet
        //        {
        //            backgroundColor = new Color(0x00, 0x00, 0x00),
        //            playerOrbitColor = new Color(0xf8, 0x00, 0xff),
        //            targetColor = new Color(0xf8, 0x00, 0xff),
        //            hurdleColor = new Color(0xd6, 0xff, 0x00),
        //            playerColor = new Color(0xf8, 0x00, 0xff),
        //            targetOrbitColor = new Color(0xf8, 0x00, 0xff),
        //            playerArrowColor = new Color(0xf8, 0x00, 0xff),
        //            scoreColor = new Color(0xf8, 0x00, 0xff, Constants.scoreAlpha),
        //            perfectTextColor = new Color(0xf8, 0x00, 0xff, Constants.perfectTextAlpha),
        //            shareBtnColor = new Color(0xf8, 0x00, 0xff),
        //            playBtnColor = new Color(0xf8, 0x00, 0xff),
        //            highscoreColor = new Color(0xf8, 0x00, 0xff),
        //            orbit1Color = new Color(0xf8, 0x00, 0xff, Constants.orbitAlpha),
        //            orbit2Color = new Color(0xf8, 0x00, 0xff, Constants.orbitAlpha),
        //            orbit3Color = new Color(0xf8, 0x00, 0xff, Constants.orbitAlpha),
        //            innerOrbitColor = new Color(0xf8, 0x00, 0xff)
        //        };
        //        ColorSet colorSet10 = new ColorSet
        //        {
        //            backgroundColor = new Color(0x00, 0x00, 0x00),
        //            playerOrbitColor = new Color(0xff, 0x00, 0x8f),
        //            targetColor = new Color(0xff, 0x00, 0x90),
        //            hurdleColor = new Color(0xaa, 0xff, 0x00),
        //            playerColor = new Color(0xff, 0x00, 0x8f),
        //targetOrbitColor = new Color(0xff, 0x00, 0x8f),
        //    playerArrowColor = new Color(0xff, 0x00, 0x8f),
        //    scoreColor = new Color(0xff, 0x00, 0x8f, Constants.scoreAlpha),
        //    perfectTextColor = new Color(0xff, 0x00, 0x8f, Constants.perfectTextAlpha),
        //    shareBtnColor = new Color(0xff, 0x00, 0x8f),
        //    playBtnColor = new Color(0xff, 0x00, 0x8f),
        //    highscoreColor = new Color(0xff, 0x00, 0x8f),
        //    orbit1Color = new Color(0xff, 0x00, 0x8f, Constants.orbitAlpha),
        //    orbit2Color = new Color(0xff, 0x00, 0x8f, Constants.orbitAlpha),
        //    orbit3Color = new Color(0xff, 0x00, 0x8f, Constants.orbitAlpha),
        //    innerOrbitColor = new Color(0xff, 0x00, 0x8f)
        //};
        //colorSets.Add(colorSet1);
        //colorSets.Add(colorSet2);
        //colorSets.Add(colorSet3);
        //colorSets.Add(colorSet4);
        //colorSets.Add(colorSet5);
        //colorSets.Add(colorSet6);
        //colorSets.Add(colorSet7);
        //colorSets.Add(colorSet8);
        //colorSets.Add(colorSet9);
        //colorSets.Add(colorSet10);

        ColorSet colorSet = new ColorSet
        {
            backgroundColor = Color.black,
            orbit1Color = Color.yellow,
            orbit2Color = Color.yellow,
            orbit3Color = Color.yellow,
            innerOrbitColor = Color.yellow,
            playerOrbitColor = Color.yellow,
            targetColor = Color.yellow,
            targetOrbitColor = Color.yellow,
            playerColor = Color.yellow,
            playerArrowColor = Color.yellow,
            scoreColor = scoreYellow,
            perfectTextColor = perfectHitYellow,
            shareBtnColor = Color.yellow,
            playBtnColor = Color.yellow,
            highscoreColor = Color.yellow,
            hurdle1Color = Color.magenta,
            hurdle2Color = Color.cyan,
            hurdle3Color = Color.red,
            hurdle4Color = Color.green
        };
        colorSets.Add(colorSet);

    }

    public ColorSet GetRandomColorSet(){
        int randomColorSet = Random.Range(0, colorSets.Count);
        Debug.Log("ColorSet: " + randomColorSet);
        return colorSets[randomColorSet];
    }
	
}
