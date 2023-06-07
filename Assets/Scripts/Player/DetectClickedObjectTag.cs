using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClickedObjectTag : MonoBehaviour
{
    [SerializeField] GameEvent onMouseHover;
    private void OnMouseOver()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        Debug.Log("tag = " + hit.collider.gameObject.tag);
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider.gameObject.tag != "")
            {
                onMouseHover.Raise(this, this.gameObject.tag);
            }
        }
    }
}
