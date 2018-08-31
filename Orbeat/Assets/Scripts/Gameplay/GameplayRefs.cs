using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayRefs : MonoBehaviour {
    
    public GameObject scoreContainer;
    public Image flashImg;
    public Color flashColor;
    public List<Image> orbitImg;
    public Image innerOrbitImg;
    public Image timerOrbitImg;
    public Camera cam;
    public Image playerOrbitImg;
    //public List<Image> targetImg;
    //public List<Image> targetOrbitImg;
    public Image playerImg;
    public Text scoreText;
    public Image playerArrowImg;
    //public Image shareBtnImg;
    public Image playBtnImg;
    public Text perfectHitText;
    //public Text highscoreText;
    public Loudness loudness;
}
