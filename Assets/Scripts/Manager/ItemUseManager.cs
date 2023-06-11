using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseManager
{

       public void UseFunction(int item){
        switch (item){
            case 1:
            PlaceStool();
            break;

            default:
            break;
        }

       }

       void PlaceStool(){
        InventoryManager.GetInstance().stoolUse.enabled = true;
        InventoryManager.GetInstance().UsedItem(1);
        StoryEvents.Instance.EventFulfill(1);
       }


}
