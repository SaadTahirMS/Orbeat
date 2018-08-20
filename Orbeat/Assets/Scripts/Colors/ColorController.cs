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
            backgroundColor = new Color(0x00, 0x00, 0x00),
            playerOrbitColor = new Color(0xff, 0x00, 0x00),
            targetColor = new Color(0xff, 0xff, 0xff),
            targetOrbitColor = new Color(0xff, 0xff, 0xff),
            playerColor = new Color(0xff, 0xff, 0xff),
            scoreColor = new Color(0xff, 0xff, 0xff, Constants.scoreAlpha),
            perfectTextColor = new Color(0xff, 0xff, 0xff,Constants.perfectTextAlpha),
            shareBtnColor = new Color(0xff, 0xff, 0xff),
            playBtnColor = new Color(0xff, 0xff, 0xff),
            highscoreColor = new Color(0xff, 0xff, 0xff),
            orbit1Color = new Color(0xff, 0xff, 0xff, Constants.orbitAlpha),
            orbit2Color = new Color(0xff, 0xff, 0xff, Constants.orbitAlpha),
            orbit3Color = new Color(0xff, 0xff, 0xff, Constants.orbitAlpha),
            innerOrbitColor = new Color(0xff, 0xff, 0xff)

        };

        ColorSet colorSet2 = new ColorSet
        {

            backgroundColor = new Color(0x00, 0x00, 0x00),
            playerOrbitColor = new Color(0xff, 0xd0, 0x01),
            targetColor = new Color(0xd0, 0xff, 0x00),
            targetOrbitColor = new Color(0xd0, 0xff, 0x00),
            playerColor = new Color(0xd0, 0xff, 0x00),
            scoreColor = new Color(0xd0, 0xff, 0x00, Constants.scoreAlpha),
            perfectTextColor = new Color(0xd0, 0xff, 0x00,Constants.perfectTextAlpha),
            shareBtnColor = new Color(0xd0, 0xff, 0x00),
            playBtnColor = new Color(0xd0, 0xff, 0x00),
            highscoreColor = new Color(0xd0, 0xff, 0x00),
            orbit1Color = new Color(0xff, 0xd0, 0x01, Constants.orbitAlpha),
            orbit2Color = new Color(0xff, 0xd0, 0x01, Constants.orbitAlpha),
            orbit3Color = new Color(0xff, 0xd0, 0x01, Constants.orbitAlpha),
            innerOrbitColor = new Color(0xff, 0xd0, 0x01)

        };

        ColorSet colorSet3 = new ColorSet
        {

            backgroundColor = new Color(0x00, 0x00, 0x00),
            playerOrbitColor = new Color(0xcf, 0xff, 0x00),
            targetColor = new Color(0xcf, 0xff, 0x00),
            targetOrbitColor = new Color(0xcf, 0xff, 0x00),
            playerColor = new Color(0xcf, 0xff, 0x00),
            scoreColor = new Color(0xcf, 0xff, 0x00, Constants.scoreAlpha),
            perfectTextColor = new Color(0xcf, 0xff, 0x00, Constants.perfectTextAlpha),
            shareBtnColor = new Color(0xcf, 0xff, 0x00),
            playBtnColor = new Color(0xcf, 0xff, 0x00),
            highscoreColor = new Color(0xcf, 0xff, 0x00),
            orbit1Color = new Color(0xcf, 0xff, 0x00, Constants.orbitAlpha),
            orbit2Color = new Color(0xcf, 0xff, 0x00, Constants.orbitAlpha),
            orbit3Color = new Color(0xcf, 0xff, 0x00, Constants.orbitAlpha),
            innerOrbitColor = new Color(0xcf, 0xff, 0x00)

        };

        ColorSet colorSet4 = new ColorSet
        {

            backgroundColor = new Color(0x00, 0x00, 0x00),
            playerOrbitColor = new Color(0x5a, 0xff, 0x00),
            targetColor = new Color(0x74, 0xed, 0x9a),
            targetOrbitColor = new Color(0x74, 0xed, 0x9a),
            playerColor = new Color(0x74, 0xed, 0x9a),
            scoreColor = new Color(0x74, 0xed, 0x9a, Constants.scoreAlpha),
            perfectTextColor = new Color(0x74, 0xed, 0x9a, Constants.perfectTextAlpha),
            shareBtnColor = new Color(0x74, 0xed, 0x9a),
            playBtnColor = new Color(0x74, 0xed, 0x9a),
            highscoreColor = new Color(0x74, 0xed, 0x9a),
            orbit1Color = new Color(0x5a, 0xff, 0x00, Constants.orbitAlpha),
            orbit2Color = new Color(0x5a, 0xff, 0x00, Constants.orbitAlpha),
            orbit3Color = new Color(0x5a, 0xff, 0x00, Constants.orbitAlpha),
            innerOrbitColor = new Color(0x5a, 0xff, 0x00)
        };

        ColorSet colorSet5 = new ColorSet
        {

            backgroundColor = new Color(0x00, 0x00, 0x00),
            playerOrbitColor = new Color(0x00, 0xff, 0x9c),
            targetColor = new Color(0x00, 0xff, 0x9c),
            targetOrbitColor = new Color(0x00, 0xff, 0x9c),
            playerColor = new Color(0x00, 0xff, 0x9c),
            scoreColor = new Color(0x00, 0xff, 0x9c, Constants.scoreAlpha),
            perfectTextColor = new Color(0x00, 0xff, 0x9c, Constants.perfectTextAlpha),
            shareBtnColor = new Color(0x00, 0xff, 0x9c),
            playBtnColor = new Color(0x00, 0xff, 0x9c),
            highscoreColor = new Color(0x00, 0xff, 0x9c),
            orbit1Color = new Color(0x00, 0xff, 0x9c, Constants.orbitAlpha),
            orbit2Color = new Color(0x00, 0xff, 0x9c, Constants.orbitAlpha),
            orbit3Color = new Color(0x00, 0xff, 0x9c, Constants.orbitAlpha),
            innerOrbitColor = new Color(0x00, 0xff, 0x9c)
        };

        ColorSet colorSet6 = new ColorSet
        {

            backgroundColor = new Color(0x00, 0x00, 0x00),
            playerOrbitColor = new Color(0x00, 0x9d, 0xff),
            targetColor = new Color(0x00, 0x9d, 0xff),
            targetOrbitColor = new Color(0x00, 0x9d, 0xff),
            playerColor = new Color(0x00, 0x9d, 0xff),
            scoreColor = new Color(0x00, 0x9d, 0xff, Constants.scoreAlpha),
            perfectTextColor = new Color(0x00, 0x9d, 0xff, Constants.perfectTextAlpha),
            shareBtnColor = new Color(0x00, 0x9d, 0xff),
            playBtnColor = new Color(0x00, 0x9d, 0xff),
            highscoreColor = new Color(0x00, 0x9d, 0xff),
            orbit1Color = new Color(0x00, 0x9d, 0xff, Constants.orbitAlpha),
            orbit2Color = new Color(0x00, 0x9d, 0xff, Constants.orbitAlpha),
            orbit3Color = new Color(0x00, 0x9d, 0xff, Constants.orbitAlpha),
            innerOrbitColor = new Color(0x00, 0x9d, 0xff)
        };

        ColorSet colorSet7 = new ColorSet
        {

            backgroundColor = new Color(0x00, 0x00, 0x00),
            playerOrbitColor = new Color(0x89, 0x00, 0xf9),
            targetColor = new Color(0x89, 0x00, 0xf9),
            targetOrbitColor = new Color(0x89, 0x00, 0xf9),
            playerColor = new Color(0x89, 0x00, 0xf9),
            scoreColor = new Color(0x89, 0x00, 0xf9, Constants.scoreAlpha),
            perfectTextColor = new Color(0x89, 0x00, 0xf9, Constants.perfectTextAlpha),
            shareBtnColor =new Color(0x89, 0x00, 0xf9),
            playBtnColor =new Color(0x89, 0x00, 0xf9),
            highscoreColor = new Color(0x89, 0x00, 0xf9),
            orbit1Color = new Color(0x89, 0x00, 0xf9, Constants.orbitAlpha),
            orbit2Color = new Color(0x89, 0x00, 0xf9, Constants.orbitAlpha),
            orbit3Color = new Color(0x89, 0x00, 0xf9, Constants.orbitAlpha),
            innerOrbitColor = new Color(0x89, 0x00, 0xf9)
        };

        ColorSet colorSet8 = new ColorSet
        {

            backgroundColor = new Color(0x00, 0x00, 0x00),
            playerOrbitColor = new Color(0x01, 0x0e, 0xff),
            targetColor = new Color(0x01, 0x0e, 0xff),
            targetOrbitColor = new Color(0x01, 0x0e, 0xff),
            playerColor = new Color(0x01, 0x0e, 0xff),
            scoreColor = new Color(0x01, 0x0e, 0xff, Constants.scoreAlpha),
            perfectTextColor = new Color(0x01, 0x0e, 0xff, Constants.perfectTextAlpha),
            shareBtnColor = new Color(0x01, 0x0e, 0xff),
            playBtnColor = new Color(0x01, 0x0e, 0xff),
            highscoreColor = new Color(0x01, 0x0e, 0xff),
            orbit1Color = new Color(0x01, 0x0e, 0xff, Constants.orbitAlpha),
            orbit2Color = new Color(0x01, 0x0e, 0xff, Constants.orbitAlpha),
            orbit3Color = new Color(0x01, 0x0e, 0xff, Constants.orbitAlpha),
            innerOrbitColor = new Color(0x01, 0x0e, 0xff)
        };

        ColorSet colorSet9 = new ColorSet
        {

            backgroundColor = new Color(0x00, 0x00, 0x00),
            playerOrbitColor = new Color(0xf8, 0x00, 0xff),
            targetColor = new Color(0xf8, 0x00, 0xff),
            targetOrbitColor = new Color(0xf8, 0x00, 0xff),
            playerColor = new Color(0xf8, 0x00, 0xff),
            scoreColor = new Color(0xf8, 0x00, 0xff, Constants.scoreAlpha),
            perfectTextColor = new Color(0xf8, 0x00, 0xff, Constants.perfectTextAlpha),
            shareBtnColor = new Color(0xf8, 0x00, 0xff),
            playBtnColor = new Color(0xf8, 0x00, 0xff),
            highscoreColor = new Color(0xf8, 0x00, 0xff),
            orbit1Color = new Color(0xf8, 0x00, 0xff, Constants.orbitAlpha),
            orbit2Color = new Color(0xf8, 0x00, 0xff, Constants.orbitAlpha),
            orbit3Color = new Color(0xf8, 0x00, 0xff, Constants.orbitAlpha),
            innerOrbitColor = new Color(0xf8, 0x00, 0xff)
        };

        ColorSet colorSet10 = new ColorSet
        {

            backgroundColor = new Color(0x00, 0x00, 0x00),
            playerOrbitColor = new Color(0xff, 0x00, 0x8f),
            targetColor = new Color(0xff, 0x00, 0x8f),
            targetOrbitColor = new Color(0xff, 0x00, 0x8f),
            playerColor = new Color(0xff, 0x00, 0x8f),
            scoreColor = new Color(0xff, 0x00, 0x8f, Constants.scoreAlpha),
            perfectTextColor = new Color(0xff, 0x00, 0x8f, Constants.perfectTextAlpha),
            shareBtnColor = new Color(0xff, 0x00, 0x8f),
            playBtnColor = new Color(0xff, 0x00, 0x8f),
            highscoreColor = new Color(0xff, 0x00, 0x8f),
            orbit1Color = new Color(0xff, 0x00, 0x8f, Constants.orbitAlpha),
            orbit2Color = new Color(0xff, 0x00, 0x8f, Constants.orbitAlpha),
            orbit3Color = new Color(0xff, 0x00, 0x8f, Constants.orbitAlpha),
            innerOrbitColor = new Color(0xff, 0x00, 0x8f)
        };

        colorSets.Add(colorSet1);
        colorSets.Add(colorSet2);
        colorSets.Add(colorSet3);
        colorSets.Add(colorSet4);
        colorSets.Add(colorSet5);
        colorSets.Add(colorSet6);
        colorSets.Add(colorSet7);
        colorSets.Add(colorSet8);
        colorSets.Add(colorSet9);
        colorSets.Add(colorSet10);

    }

    public ColorSet GetRandomColorSet(){
        int randomColorSet = Random.Range(0, colorSets.Count);
        return colorSets[randomColorSet];
    }
	
}
