using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementLightFlicker : MonoBehaviour
{
    [SerializeField] float minTime;
    [SerializeField] float maxTime;

    SpriteRenderer bsmntLight;
    bool lightOn = true;
    bool canChange = true;
    // Start is called before the first frame update
    void Start()
    {
        bsmntLight = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canChange){
            StartCoroutine("FlickerLight");
        }
        
    }

    IEnumerator FlickerLight(){
        canChange = false;
        float waitTime = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(waitTime);
        lightOn = !lightOn;
        bsmntLight.enabled = lightOn;
        canChange = true;

    }
}
