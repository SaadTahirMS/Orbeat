using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Loudness : MonoBehaviour
{

    public AudioSource audioSource;
    public float updateStep = 0.03f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    private float clipLoudness;
    private float[] clipSampleData;

    public bool canBeat = false;
    public bool playAlone = false;
    MyBeat[] mb; //it finds all MyBeat itself
    MyFade[] mf; //it finds all MyFade itself
    MyCamZoom[] mcz;

    //public List<AudioClip> songs;
    //int songCount = 0;

    public void Initialize()
    {
        mb = FindObjectsOfType<MyBeat>();
        mf = FindObjectsOfType<MyFade>();
        mcz = FindObjectsOfType<MyCamZoom>();

        if (!audioSource)
        {
            Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
        }
        clipSampleData = new float[sampleDataLength];
        canBeat = true;
    }

    void Awake()
    {

        if (playAlone)
        {
            mb = FindObjectsOfType<MyBeat>();
            if (!audioSource)
            {
                Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
            }
            clipSampleData = new float[sampleDataLength];
        }
        //NextSong();

    }
    // Update is called once per frame
    void Update()
    {
        if (canBeat)
        {
            currentUpdateTime += Time.deltaTime;
            if (currentUpdateTime >= updateStep)
            {
                currentUpdateTime = 0f;
                audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
                clipLoudness = 0f;
                foreach (var sample in clipSampleData)
                {
                    clipLoudness += Mathf.Abs(sample);
                }
                clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
                DOBeat(clipLoudness);
                DOFade(clipLoudness);
                DOCamZoom(clipLoudness);
            }

            //if (Input.GetMouseButtonDown(0))
            //{
            //    audioSource.pitch = 1 - pitchEndValue;
            //}
            //else if (Input.GetMouseButtonDown(1))
            //{
            //    audioSource.pitch = 1 + pitchEndValue;
            //}
            //else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            //{
            //    audioSource.pitch = 1;
            //}
        }
    }

    //public void NextSong()
    //{
    //    audioSource.enabled = false;
    //    if (songCount >= songs.Count)
    //    {
    //        songCount = 0;
    //    }
    //    audioSource.clip = songs[songCount];
    //    songCount++;
    //    audioSource.enabled = true;
    //}

    void DOBeat(float v)
    {
        for (int i = 0; i < mb.Length; i++)
            mb[i].DoBeat(v);
    }
    void DOFade(float v)
    {
        for (int i = 0; i < mf.Length; i++)
            mf[i].DoFade(v);
    }
    void DOCamZoom(float v)
    {
        for (int i = 0; i < mcz.Length; i++)
            mcz[i].DoZoom(v);
    }
}
