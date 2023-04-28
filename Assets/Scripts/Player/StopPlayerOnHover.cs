using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StopPlayerOnHover : MonoBehaviour
{
    [SerializeField] GameEvent StopPlayer;
    [SerializeField] GameEvent ResumePlayer;
    bool needChange = false;

    bool isMouseOverUi = false;

    int UILayer;

    private void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");
    }

    private void Update()
    {
        Debug.Log("need change " + needChange);
        IsPointerOverUIElement();
        if (needChange)
        {
            if (isMouseOverUi)
            {
                StopPlayer.Raise(this, "");
            }
            else
            {
                ResumePlayer.Raise(this, "");
            }
            needChange = false;
        }
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
            {
                Debug.Log("is mouse over ui" + isMouseOverUi);
                if (isMouseOverUi == false)
                    needChange = true;
                isMouseOverUi = true;
                return true;
            }
        }
        Debug.Log("is mouse over ui" + isMouseOverUi);
        if (isMouseOverUi)
            needChange = true;
        isMouseOverUi = false;
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
