using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseManager
{
    public GameEvent itemToPot;

    private static ItemUseManager instance;

    StoryEvents storyEvents = new StoryEvents();

    public void UseFunction(int item)
    {
        switch (item)
        {
            case 1:
                PlaceStool();
                break;

            case 2:
                SpoonTheRecipe();
                break;

            case 8:
                ItemToPot(item);
                break;

            default:
                break;
        }

    }

    void PlaceStool()
    {
        InventoryManager.GetInstance().stoolUse.enabled = true;
        InventoryManager.GetInstance().UsedItem(1);
        storyEvents.EventFulfill(2);
    }

    void SpoonTheRecipe()
    {
        GameManager.GetInstance().GotRecipe();
        InventoryManager.GetInstance().box.SetActive(false);
        InventoryManager.GetInstance().UsedItem(2);
        storyEvents.EventFulfill(1);
    }

    void ItemToPot(int item)
    {
        GameManager.GetInstance().ItemToPot(item);
        InventoryManager.GetInstance().UsedItem(item);
    }


}
