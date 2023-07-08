using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    [SerializeField] int[] itemIdToUse;
    [SerializeField] int[] eventsPrecondition;
    [SerializeField] private TextAsset cannotUseYetInk;
    [SerializeField] GameEvent mouseOver;
    [SerializeField] GameEvent startConvo;
    [SerializeField] GameEvent changeCurser;

    ItemUseManager itemsManager = new ItemUseManager();
    StoryEvents storyEvents = new StoryEvents();

    public void ClickedPlaceToUse()
    {
        Debug.Log("clicked item space to use");
        Debug.Log("item selected in manager: " + GameManager.GetInstance().GetSelectedItem());
        startConvo.Raise(this, "dialogue");
        for (int i = 0; i < itemIdToUse.Length; i++)
        {
            if (GameManager.GetInstance().GetSelectedItem() == itemIdToUse[i])
            {
                for (int j = 0; j < eventsPrecondition.Length; j++)
                {
                    if (storyEvents.CheckPrecondition(eventsPrecondition[j]))
                    {
                        if (!DialogueManager.GetInstance().dialogueIsPlaying) DialogueManager.GetInstance().EnterDialogueMode(cannotUseYetInk);
                        return;
                    }

                }

                itemsManager.UseFunction(itemIdToUse[i]);
            }
        }

        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            Debug.Log("can start no item dialogue");
            DialogueManager.GetInstance().EnterDialogueMode(cannotUseYetInk);
        }

    }

    private void OnMouseEnter()
    {
        Debug.Log("mouse entered speech area");
        mouseOver.Raise(this, this.gameObject.tag);
        changeCurser.Raise(this, this.gameObject.tag);

    }

    private void OnMouseExit()
    {
        mouseOver.Raise(this, "");
        changeCurser.Raise(this, "");
    }




}
