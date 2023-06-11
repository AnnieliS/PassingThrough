using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioPlayer;

    [SerializeField] AudioClip[] clips;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    int pick;
    bool canPlay = true;


    // Update is called once per frame
    void Update()
    {
        if(!audioPlayer.isPlaying && canPlay){
            StartCoroutine("PlayRandomSound");
        }
        
    }

    IEnumerator PlayRandomSound(){
        canPlay = false;
        pick = (int)Random.Range(0, clips.Length);
        audioPlayer.clip = clips[pick];
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        audioPlayer.Play();
        canPlay = true;
        

    }
}
