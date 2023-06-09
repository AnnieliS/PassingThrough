using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIntoPositionPlaceCheck : MonoBehaviour
{
    [SerializeField] List<RotateIntoPosition> parts;
    [SerializeField] GameEvent finishPuzzle;
    [SerializeField] GameObject miniGame;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "rotatePlaceCheck")
        {
            RotateIntoPosition codeCheck;
            codeCheck = other.gameObject.GetComponentInParent<RotateIntoPosition>();
            // Debug.Log(codeCheck.GetMoving());
            if (!codeCheck.GetMoving())
            {
                codeCheck.InPosition();

                if (CheckForPuzzleSuccess())
                {
                    // Debug.Log("get the puzzle reward");
                    StartCoroutine(FinishPuzzle());
                }


            }
        }
    }

    bool CheckForPuzzleSuccess()
    {
        foreach (RotateIntoPosition part in parts)
        {
            if (!part.GetComponent<RotateIntoPosition>().GetFinished())
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator FinishPuzzle()
    {
        yield return new WaitForSeconds(2f);
        finishPuzzle.Raise(this, miniGame);

    }


}
