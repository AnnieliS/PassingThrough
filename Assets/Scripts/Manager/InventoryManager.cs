using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] int maxInventoryShow;
    [SerializeField] GameObject[] inventorySpace;
    public List<CollectibleItem> allItems = new List<CollectibleItem>();
    int offset = 0;

    public void PickedUp(Component sender, object data){
        CollectibleItem item = (CollectibleItem)sender;
        allItems.Add(item);
        RefreshView();
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
    }
}
