using UnityEngine;
using Ink.Runtime;

public class GameManager : MonoBehaviour
{
    
#region Canvases
    [SerializeField] GameObject quitCanvas;
    [SerializeField] GameObject inventoryCanvas;
#endregion

#region cameras
    [Header("Cameras")]
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera puzzleCamera;
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
        ResetCameras();
    }

    void ResetCameras(){
        mainCamera.enabled = true;
        puzzleCamera.enabled = false;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    #endregion

    #region puzzle functions

    public void SwitchToPuzzleCamera(){
        mainCamera.enabled = false;
        puzzleCamera.enabled = true;
    }

        public void ActivatePuzzle(Component sender, object data)
    {
        GameObject puzzle = (GameObject)data;
        Debug.Log("click puzzle: " + puzzle.name);
        inventoryCanvas.SetActive(false);
        puzzle.GetComponent<PuzzleActivation>().miniGame.SetActive(true);
        
    }

     public void DeactivatePuzzle(Component sender, object data){
        GameObject puzzle = (GameObject)data;
        puzzle.SetActive(false);
        inventoryCanvas.SetActive(true);
        ResetCameras();
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
