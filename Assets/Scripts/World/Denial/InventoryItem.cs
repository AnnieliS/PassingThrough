using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] GameEvent selectItem;
    public int itemId;

    public void ChooseItem(){
        GameManager.GetInstance().SetSelectedItem(itemId);
        // Debug.Log(GameManager.GetInstance().GetSelectedItem());
    }
}
