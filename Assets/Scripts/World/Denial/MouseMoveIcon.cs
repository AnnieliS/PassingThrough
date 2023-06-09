using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMoveIcon : MonoBehaviour
{
   [SerializeField] GameEvent changeCursor;

   private void OnMouseEnter() {
    changeCursor.Raise(this, "teleport");
   }

   private void OnMouseExit() {
    changeCursor.Raise(this, "def");
   }
}
