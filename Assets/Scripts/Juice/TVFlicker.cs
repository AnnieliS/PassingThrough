using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TVFlicker : MonoBehaviour
{
    [SerializeField] Light2D light2d;
    [SerializeField] Color[] possibleColors;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    int numOfColors;
    bool isFlickering = false;
    float timeDelay;
    void Start()
    {
        numOfColors = possibleColors.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFlickering){
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight(){
        isFlickering = true;
        // light2d.enabled = false;
        timeDelay = Random.Range(minTime, maxTime);
        int colorSelected = Mathf.FloorToInt(Random.Range(0, numOfColors));
        light2d.color = possibleColors[colorSelected];
        yield return new WaitForSeconds(timeDelay);
        // light2d.enabled = true;
        // timeDelay = Random.Range(minTime, maxTime);
        // yield return new WaitForSeconds(timeDelay);
        isFlickering = false;


    }
}
