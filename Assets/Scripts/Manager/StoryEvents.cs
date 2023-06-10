using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvents
{
    public static StoryEvents Instance { get; private set; }

    public bool isChairInPlace = false;
    public bool gotRecipe = false;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Debug.LogError("There is already a storyevents instance");
        }
        else
        {
            Instance = this;
        }
    }


}
