using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClickedObjectTag : MonoBehaviour
{
    [SerializeField] GameEvent onMouseHover;
    [SerializeField] GameEvent changeCursos;
    private void OnMouseHover()
    {
        // Debug.Log("tag gameobject= " + this.gameObject.tag);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        // Debug.Log("tag raycast= " + hit.collider.gameObject.tag);
        if (hit.collider.gameObject.tag != "")
        {
            // Debug.Log("entered object");
            onMouseHover.Raise(this, hit.collider.gameObject.tag);
            changeCursos.Raise(this, hit.collider.gameObject.tag);

        }
        // if (Input.GetMouseButtonDown(0))
        // {
        //     if (hit.collider.gameObject.tag != "")
        //     {
        //     }
        // }
    }
}
