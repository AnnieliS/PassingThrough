using UnityEngine;
using Ink.Runtime;

public class GameManager : MonoBehaviour
{
    
#region Canvases
    [SerializeField] GameObject quitCanvas;
#endregion

    #region  Events
    [Header("events")]

    #endregion
    private static GameManager instance;

#region init params
    bool quitCanvasOpen = false;
#endregion

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
        quitCanvas.SetActive(false);

    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    #endregion

    
    
    #region quit functions

    public void ToggleQuitCanvas(){
        quitCanvasOpen = !quitCanvasOpen;
        quitCanvas.SetActive(quitCanvasOpen);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

}
