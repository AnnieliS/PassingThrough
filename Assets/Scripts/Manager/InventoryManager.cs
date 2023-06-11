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
    public List<CollectibleItem> allItems = new List<CollectibleItem>();
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

    public void PickedUp(Component sender, object data){
        CollectibleItem item = (CollectibleItem)sender;
        allItems.Add(item);
        RefreshView();
    }

    public void UsedItem(int itemid){
        foreach(CollectibleItem item in allItems){
            if(item.id == itemid){
                allItems.Remove(item);
                 RefreshView();
            }
        }

    }

    private void RefreshView(){
        int tempOffset = offset;
        int i = 0;
        foreach(CollectibleItem item in allItems){
            if(tempOffset > 0){
                tempOffset--;
            }
            else{
                Image tempImage;
                tempImage = inventorySpace[i].GetComponent<Image>();
                tempImage.sprite = item.inventorySprite;
                tempImage.color = new Color(1,1,1,1);
                
                inventorySpace[i].GetComponent<InventoryItem>().itemId = item.id;
                i++;
            }
        }

        for( int j = i ; j < inventorySpace.Length ; j++){
            Image tempImage;
                tempImage = inventorySpace[j].GetComponent<Image>();
                tempImage.sprite = null;
                tempImage.color = new Color(0,0,0,0);
                
                inventorySpace[j].GetComponent<InventoryItem>().itemId = 0;

        }
    }
}
