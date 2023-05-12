using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [Header("Item info")]
    public Sprite worldSprite;
    public Sprite hoverSprite;
    public Sprite inventorySprite;
    public int id;
    [Header("Pickup Event")]
    [SerializeField] GameEvent pickupEvent;
    [SerializeField] GameObject itemCanvas;

    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = worldSprite;
    }

    public void PickupItem()
    {
        pickupEvent.Raise(this, gameObject);
        itemCanvas.SetActive(true);
        itemCanvas.GetComponent<FadeInAndOut>().Activate(inventorySprite);
        Destroy(gameObject);
    }
    
    private void OnMouseEnter() {
        spriteRenderer.sprite = hoverSprite;
    }

    private void OnMouseExit() {
        spriteRenderer.sprite = worldSprite;
    }
}