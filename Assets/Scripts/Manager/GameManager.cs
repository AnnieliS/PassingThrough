using UnityEngine;
using Ink.Runtime;

public class GameManager : MonoBehaviour
{
    

    #region  Events
    [Header("events")]

    #endregion
    private static GameManager instance;

    #region inits
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Game Manager in the scene");
        }
        instance = this;
    }
    private void Start()
    {
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    #endregion

    
    
    #region quit functions
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

}
