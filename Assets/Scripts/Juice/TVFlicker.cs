using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TVFlicker : MonoBehaviour
{
    [Header("Pickable")]
    [SerializeField] GameObject matches;

    [Header("Events")]
    [SerializeField] GameEvent onHover;
    [SerializeField] GameEvent changeCursor;

    [Header("TV Settings")]
    [SerializeField] Light2D light2d;
    [SerializeField] int numOfChannel;
    [SerializeField] Color[] channel1;
    [SerializeField] Color[] channel2;
    [SerializeField] Color[] channel3;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    Color[] currentChannel;
    int numOfColors;
    int channel = 0;
    bool isFlickering = false;
    float timeDelay;
    void Start()
    {
        numOfColors = channel1.Length;
        currentChannel = (Color[])channel1.Clone();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFlickering)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        // light2d.enabled = false;
        timeDelay = Random.Range(minTime, maxTime);
        int colorSelected = Mathf.FloorToInt(Random.Range(0, numOfColors));
        light2d.color = currentChannel[colorSelected];
        yield return new WaitForSeconds(timeDelay);
        // light2d.enabled = true;
        // timeDelay = Random.Range(minTime, maxTime);
        // yield return new WaitForSeconds(timeDelay);
        isFlickering = false;


    }

    public void ChangeChangeChannel(Component sender, object data)
    {
        channel++;
        if (channel == numOfChannel) channel = 0;

        switch (channel)
        {
            case 0:
                currentChannel = (Color[])channel1.Clone();
                matches.SetActive(false);
                break;

            case 1:
                currentChannel = (Color[])channel2.Clone();
                matches.SetActive(false);
                break;

            case 2:
                currentChannel = (Color[])channel3.Clone();
                matches.SetActive(true);
                break;

            default:
                currentChannel = (Color[])channel1.Clone();
                break;
        }

    }

    private void OnMouseEnter()
    {
        onHover.Raise(this, this.gameObject.tag);
        changeCursor.Raise(this, this.gameObject.tag);
    }

    private void OnMouseExit()
    {
        onHover.Raise(this, "");
        changeCursor.Raise(this, "");
    }
}
