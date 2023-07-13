using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PotScenesChange : MonoBehaviour
{
    [SerializeField] GameObject clipCanvas;

    [Header("Clips")]
    [SerializeField] VideoClip idle;
    [SerializeField] VideoClip earth;
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
            case -1:
                return;
            case 6:
                PlayClip(earth);
                break;
            case 8:
                PlayClip(match);
                break;

            case 9:
                PlayClip(metal);
                break;
            case 10:
                PlayClip(water);
                break;
            case 11:
                PlayClip(oil);
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
        videoPlayer.clip = idle;
    }



}
