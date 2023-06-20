using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public GameObject destinationPoint;

    [SerializeField] GameEvent mouseOver;
    public Sprite teleportImage;
    

    private void OnMouseEnter() {
        mouseOver.Raise(this, this.gameObject.tag);
    }

    private void OnMouseExit() {
        mouseOver.Raise(this, "");
    }

}
