using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvents
{
    public static StoryEvents Instance { get; private set; }

    public bool isChairInPlace = false; // event 1
    public bool gotRecipe = false; // event 2

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

    public bool CheckPrecondition(int preconditionID)
    {

        switch (preconditionID)
        {
            case 1:
                return isChairInPlace;


            case 2:
                return gotRecipe;


            default:
                return false;
        }

    }

    public void EventFulfill(int eventID)
    {
        switch (eventID)
        {
            case 1:
                isChairInPlace = true;
                break;


            case 2:
                gotRecipe = true;
                break;


            default:
                break;
        }
    }




}
