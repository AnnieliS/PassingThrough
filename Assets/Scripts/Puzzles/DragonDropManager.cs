using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDropManager : MonoBehaviour
{
    [SerializeField] List<DragonDropIntoPosition> parts;
    [SerializeField] GameEvent finishPuzzle;
    [SerializeField] GameObject miniGame;
    private static DragonDropManager instance;
    bool isOverPiece;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Game Manager in the scene");
        }
        instance = this;
    }

    public void PieceInRightPlace(){
        if(CheckForPuzzleSuccess()){
            StartCoroutine(FinishPuzzle());
        }
        
    }

       bool CheckForPuzzleSuccess()
    {
        foreach (DragonDropIntoPosition part in parts)
        {
            if (!part.GetComponent<DragonDropIntoPosition>().GetFinished())
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

    public void OverPiece(Component sender, object data)
    {
        isOverPiece = true;
    }

    public void NotOverPiece(Component sender, object data)
    {
        isOverPiece = false;
    }

    public bool GetOverPiece()
    {
        return isOverPiece;
    }
}
