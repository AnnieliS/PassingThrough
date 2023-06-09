using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] GameEvent selectItem;
    public int itemId;

    public void ChooseItem(){
        selectItem.Raise(this, itemId);
        Debug.Log("item: " + itemId);
    }
}
