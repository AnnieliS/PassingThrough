using UnityEngine;
using Ink.Runtime;

public class GameManager : MonoBehaviour
{

    #region Canvases
    [SerializeField] GameObject quitCanvas;
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] GameObject inventoryButtonCanvas;
    #endregion

    #region cameras
    [Header("Cameras")]
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera puzzleCamera;
    #endregion

    #region texts
    [Header("Ink JSON Text Files")]
    [SerializeField] private TextAsset initalDialogue;

    #endregion

    #region  Events
    [Header("events")]
    [SerializeField] GameEvent dialogueStart;
    [SerializeField] GameEvent switchClicktagToDialogue;
    [SerializeField] GameEvent pausePlayer;
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
        inventoryButtonCanvas.SetActive(true);
        inventoryCanvas.SetActive(false);
        ResetCameras();
        StartingDialogue();
    }

    void ResetCameras()
    {
        mainCamera.enabled = true;
        puzzleCamera.enabled = false;
    }

    void StartingDialogue()
    {
        pausePlayer.Raise(this, "");
        switchClicktagToDialogue.Raise(this, "dialogue");
        dialogueStart.Raise(this, "dialogue");
        DialogueManager.GetInstance().EnterDialogueMode(initalDialogue);

    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    #endregion

    #region puzzle functions

    public void SwitchToPuzzleCamera()
    {
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

    public void DeactivatePuzzle(Component sender, object data)
    {
        GameObject puzzle = (GameObject)data;
        puzzle.SetActive(false);
        inventoryCanvas.SetActive(true);
        ResetCameras();
    }

    #endregion


    #region  inventory functions

    public void ShowInventory()
    {
        inventoryCanvas.SetActive(true);
        inventoryButtonCanvas.SetActive(false);
    }

    public void HideInventory()
    {
        inventoryCanvas.SetActive(false);
        inventoryButtonCanvas.SetActive(true);
    }

    #endregion

    #region quit functions

    public void ToggleQuitCanvas()
    {
        quitCanvasOpen = !quitCanvasOpen;
        quitCanvas.SetActive(quitCanvasOpen);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

}
