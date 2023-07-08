using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PotScenesChange : MonoBehaviour
{
    [SerializeField] GameObject clipCanvas;
    [SerializeField] float showTime;

    [Header("Clips")]
    [SerializeField] VideoClip earth;
    [SerializeField] VideoClip idle;
    [SerializeField] VideoClip match;
    [SerializeField] VideoClip metal;
    [SerializeField] VideoClip oil;
    [SerializeField] VideoClip water;

    VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += HideClip;
    }


    public void PlayCutscene(Component sender, object data)
    {
        int item = (int)data;
        switch (item)
        {
            case 8:
                PlayClip(match);
                break;

            default:
                PlayClip(idle);

                break;
        }
    }

    void PlayClip(VideoClip clip)
    {
        videoPlayer.clip = clip;
        clipCanvas.SetActive(true);
    }

    void HideClip(VideoPlayer source)
    {
        clipCanvas.SetActive(false);
    }



}
