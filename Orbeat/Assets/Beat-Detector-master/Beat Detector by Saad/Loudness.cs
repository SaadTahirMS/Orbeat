using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Loudness : MonoBehaviour
{
    public float pitchDuration,pitchEndValue;
    public bool playAlone = false;
    public AudioSource audioSource;
    public float updateStep = 0.03f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    private float clipLoudness;
    private float[] clipSampleData;

    MyBeat[] cb;

    public void Initialize()
    {
        //cb = new MyBeat[50];
        cb = FindObjectsOfType<MyBeat>();
        if (!audioSource)
        {
            Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
        }
        clipSampleData = new float[sampleDataLength];
    }

    public void Awake()
    {
        if(playAlone){
            cb = FindObjectsOfType<MyBeat>();
            if (!audioSource)
            {
                Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
            }
            clipSampleData = new float[sampleDataLength];
        }
    }
    float pitch;
    // Update is called once per frame
    void Update()
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

        }

        if (Input.GetMouseButtonDown(0))
        {
            audioSource.DOPitch(pitchEndValue, pitchDuration);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            audioSource.DOPitch(1 + pitchEndValue, pitchDuration);
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)){
            audioSource.DOPitch(1, pitchDuration);
        }
    }

    void DOBeat(float v)
    {
        for (int i = 0; i < cb.Length; i++)
            cb[i].DoBeat(v);
    }


}
