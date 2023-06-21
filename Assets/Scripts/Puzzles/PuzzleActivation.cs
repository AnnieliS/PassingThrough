using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleActivation : MonoBehaviour
{
    [Header("Mini Game Object")]
    public GameObject miniGame;
    [SerializeField] GameEvent detectMouse;
    [SerializeField] GameEvent changeCursor;

    private void OnMouseEnter()
    {
            detectMouse.Raise(this, "puzzle");
            changeCursor.Raise(this, this.gameObject.tag);
    }

    private void OnMouseExit(){
        detectMouse.Raise(this, "");
        changeCursor.Raise(this, "");
    }





}
