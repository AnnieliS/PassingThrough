using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvents : MonoBehaviour
{
    private static StoryEvents Instance;

    public bool gotRecipe = false; // event 1
    public bool isChairInPlace = false; // event 2

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

     public static StoryEvents GetInstance()
    {
        return Instance;
    }

    public bool CheckPrecondition(int preconditionID)
    {

        switch (preconditionID)
        {
            case 1:
                return gotRecipe;


            case 2:
                return isChairInPlace;


            default:
                return false;
        }

    }

    public void EventFulfill(int eventID)
    {
        switch (eventID)
        {
            case 1:
                gotRecipe = true;
                break;


            case 2:
                isChairInPlace = true;
                break;


            default:
                break;
        }
    }




}
