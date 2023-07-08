using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnTap : MonoBehaviour
{
    [SerializeField] GameEvent mouseHover;
    [SerializeField] GameEvent cursorChange;

    private void OnMouseEnter() {
        mouseHover.Raise(this, this.gameObject.tag);
        cursorChange.Raise(this, this.gameObject.tag);
    }
    
    private void OnMouseExit(){
        mouseHover.Raise(this, "");
        cursorChange.Raise(this, "");
    }
}
