using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseManager : MonoBehaviour
{

    private static ItemUseManager instance;

    // StoryEvents storyEvents = new StoryEvents();

    public void UseFunction(GameObject sender, int item)
    {
        switch (item)
        {
            case 1:
                PlaceStool();
                break;

            case 2:
                SpoonTheRecipe();
                break;

            case 4:
                MeltLead();
                break;

            case 6:
                GameManager.GetInstance().EventFulfill(4);
                ItemToPot(item);
                break;
            case 7:
                BucketToFloor(sender);
                break;

            case 8:
                GameManager.GetInstance().EventFulfill(7);
                ItemToPot(item);
                break;
            case 9:
                GameManager.GetInstance().EventFulfill(8);
                ItemToPot(item);
                break;
            case 10:
                GameManager.GetInstance().EventFulfill(5);
                ItemToPot(item);
                break;
            case 11:
                GameManager.GetInstance().EventFulfill(6);
                ItemToPot(item);
                break;

            case 12:
            GameManager.GetInstance().EndCutscene();
            break;

            default:
                break;
        }

    }

    void PlaceStool()
    {
        GameManager.GetInstance().EventFulfill(2);
        InventoryManager.GetInstance().stoolUse.enabled = true;
        InventoryManager.GetInstance().UsedItem(1);
        GameManager.GetInstance().SetSelectedItem(-1);
    }

    void SpoonTheRecipe()
    {
        GameManager.GetInstance().EventFulfill(1);
        GameManager.GetInstance().GotRecipe();
        InventoryManager.GetInstance().box.SetActive(false);
        InventoryManager.GetInstance().UsedItem(2);
        GameManager.GetInstance().SetSelectedItem(-1);
    }

    void MeltLead()
    {
        GameManager.GetInstance().MeltLead();
        InventoryManager.GetInstance().UsedItem(4);
        GameManager.GetInstance().SetSelectedItem(-1);
    }

    void BucketToFloor(GameObject bucket)
    {
        bucket.GetComponent<BucketThingy>().PutBucketDown();
        InventoryManager.GetInstance().UsedItem(7);
        GameManager.GetInstance().SetSelectedItem(-1);
    }

    void ItemToPot(int item)
    {
        GameManager.GetInstance().ItemToPot(item);
        InventoryManager.GetInstance().UsedItem(item);
        GameManager.GetInstance().SetSelectedItem(-1);
    }


}
