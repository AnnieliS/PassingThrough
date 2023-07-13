using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TurnOffVideo : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    VideoPlayer vp;
    // Start is called before the first frame update
    void Start()
    {  
        vp = GetComponent<VideoPlayer>();
        vp.loopPointReached += EndVideo;
        Cursor.visible = false;
        
    }

    public void PlayVideo(){
        canvas.SetActive(true);
        vp.Play();
    }

    void EndVideo(VideoPlayer source){
        canvas.SetActive(false);
        Cursor.visible = true;
        vp.Stop();
    }
}
