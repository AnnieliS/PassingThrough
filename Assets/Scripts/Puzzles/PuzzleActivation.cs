using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleActivation : MonoBehaviour
{
    [Header("Mini Game Object")]
    public GameObject miniGame;
    [SerializeField] GameEvent detectMouse;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            detectMouse.Raise(this, "puzzle");
    }





}
