using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDropManager : MonoBehaviour
{
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

    public void OverPiece(Component sender, object data){
        isOverPiece = true;
    }

    public void NotOverPiece(Component sender, object data){
        isOverPiece = false;
    }

    public bool GetOverPiece(){
        return isOverPiece;
    }
}
