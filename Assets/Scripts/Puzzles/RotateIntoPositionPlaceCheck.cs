using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIntoPositionPlaceCheck : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "rotatePlaceCheck")
        {
            RotateIntoPosition codeCheck;
            codeCheck = other.gameObject.GetComponentInParent<RotateIntoPosition>();
            Debug.Log(codeCheck.GetMoving());
            if (!codeCheck.GetMoving())
            {
                codeCheck.InPosition();
            }
        }
    }
}
