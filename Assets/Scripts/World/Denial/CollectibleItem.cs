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
    [SerializeField] GameEvent mouseChange;
    [SerializeField] GameEvent onMouseHover;

    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = worldSprite;
    }

    public void PickupItem()
    {
        itemCanvas.SetActive(true);
        itemCanvas.GetComponent<FadeInAndOut>().Activate(inventorySprite);
        itemCanvas.GetComponent<FadeInAndOut>().Activate2();
        pickupEvent.Raise(this, gameObject);
        Destroy(gameObject);
    }
    
    private void OnMouseEnter() {
        spriteRenderer.sprite = hoverSprite;
        onMouseHover.Raise(this, this.gameObject.tag);
        mouseChange.Raise(this, "item");

    }

    private void OnMouseExit() {
        spriteRenderer.sprite = worldSprite;
        onMouseHover.Raise(this, "");
        mouseChange.Raise(this, "def");
    }
}
