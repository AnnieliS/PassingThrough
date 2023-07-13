using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] int maxInventoryShow;
    [SerializeField] GameObject[] inventorySpace;

    [Header("Item Use Places")]
    public SpriteRenderer stoolUse;
    public GameObject box;
    public List<CollectibleItem> allItems = new List<CollectibleItem>();
    //StoryEvents storyEvents = new StoryEvents();
    private static InventoryManager instance;
    int offset = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Game Manager in the scene");
        }
        instance = this;
    }

    public static InventoryManager GetInstance()
    {
        return instance;
    }

    public void PickedUp(Component sender, object data)
    {
        CollectibleItem item = (CollectibleItem)sender;
        allItems.Add(item);
        RefreshView();
    }

    public void UsedItem(int itemid)
    {
        // bool canPutLead = true;
        foreach (CollectibleItem item in allItems)
        {
            if (item.id == itemid)
            {
                allItems.Remove(item);
                // if(item.id == 6 || item.id == 8 || item.id == 10 || item.id == 11){
                //     canPutLead = false;
                // }
                RefreshView();
            }
        }
        // if(canPutLead){
        //     GameManager.GetInstance().EventFulfill(4);
        // }

    }

    private void RefreshView()
    {
        int tempOffset = offset;
        int i = 0;
        foreach (CollectibleItem item in allItems)
        {
            if (tempOffset > 0)
            {
                tempOffset--;
            }
            else
            {
                Image tempImage;
                if (i < inventorySpace.Length)
                {
                    tempImage = inventorySpace[i].GetComponent<Image>();
                    tempImage.sprite = item.inventorySprite;
                    tempImage.color = new Color(1, 1, 1, 1);

                    inventorySpace[i].GetComponent<InventoryItem>().itemId = item.id;
                }
                i++;
            }
        }

        for (int j = i; j < inventorySpace.Length; j++)
        {
            Image tempImage;
            tempImage = inventorySpace[j].GetComponent<Image>();
            tempImage.sprite = null;
            tempImage.color = new Color(0, 0, 0, 0);

            inventorySpace[j].GetComponent<InventoryItem>().itemId = 0;

        }
    }

    public void PressRight()
    {
        // Debug.Log(allItems.Count);
        offset++;
        if (offset > allItems.Count - inventorySpace.Length) offset = 0;
        RefreshView();
    }

    public void PressLeft()
    {
        offset--;
        if (offset < 0) offset = allItems.Count - 1;
        RefreshView();
    }
}
