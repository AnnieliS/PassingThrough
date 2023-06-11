using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    [SerializeField] int itemIdToUse;
    [SerializeField] int[] eventsPrecondition;
    [SerializeField] private TextAsset cannotUseYetInk;
    [SerializeField] GameEvent mouseOver;
    [SerializeField] GameEvent startConvo;

    ItemUseManager itemsManager = new ItemUseManager();
    StoryEvents storyEvents = new StoryEvents();

    public void ClickedPlaceToUse()
    {
        Debug.Log("clicked item space to use");
        Debug.Log("item selected in manager: " + GameManager.GetInstance().GetSelectedItem());
        if (GameManager.GetInstance().GetSelectedItem() == itemIdToUse)
        {
            for (int i = 0; i < eventsPrecondition.Length; i++)
            {
                Debug.Log("object precondition: " + eventsPrecondition[i]);
                Debug.Log("precondiyion Check: " + storyEvents.CheckPrecondition(2));
                if (storyEvents.CheckPrecondition(eventsPrecondition[i]))
                {
                    if (!DialogueManager.GetInstance().dialogueIsPlaying) DialogueManager.GetInstance().EnterDialogueMode(cannotUseYetInk);
                    return;
                }

            }

            itemsManager.UseFunction(itemIdToUse);
        }
        else
        {
            if (!DialogueManager.GetInstance().dialogueIsPlaying)
            {
                Debug.Log("can start no item dialogue");
                DialogueManager.GetInstance().EnterDialogueMode(cannotUseYetInk);
            }
        }
    }

    private void OnMouseEnter()
    {
        startConvo.Raise(this, "dialogue");
        mouseOver.Raise(this, this.gameObject.tag);
    }

    private void OnMouseExit()
    {
        mouseOver.Raise(this, "");
    }




}
