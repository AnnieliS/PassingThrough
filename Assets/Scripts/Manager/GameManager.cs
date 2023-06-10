using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{

    #region Canvases
    [SerializeField] GameObject quitCanvas;
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] GameObject inventoryButtonCanvas;
    [SerializeField] GameObject getRecipeCanvas;
    [SerializeField] GameObject recipeCanvas;
    #endregion

    #region cameras
    [Header("Cameras")]
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera puzzleCamera;
    #endregion

    #region cursors
    [Header("Mouse Cursors")]
    [SerializeField] Texture2D defCursor;
    [SerializeField] Texture2D teleportCursor;
    [SerializeField] Texture2D itemCursor;
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
    private StoryEvents storyEvents;

    #region Recipe
    [Header("recipe")]
    [SerializeField] GameObject recipeButton;
    [SerializeField] GameObject recipeCutsceneImage;
    [SerializeField] GameObject recipeTextImage;



    #endregion

    #region init params
    bool quitCanvasOpen = false;
    bool recipeCanvasOpen = false;
    Animation getRecipeAnim;

    private int selectedItem;
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
        ResetRecipe();
        Cursor.SetCursor(defCursor, new Vector2(-0.5f, 0.5f), CursorMode.Auto);


        storyEvents = new StoryEvents();

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

    void ResetRecipe(){
        getRecipeCanvas.SetActive(false);
        getRecipeAnim = getRecipeCanvas.GetComponentInChildren<Animation>();
        // recipeButton.SetActive(false);
        recipeCutsceneImage.SetActive(true);
        recipeTextImage.SetActive(false);
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
        // Debug.Log("click puzzle: " + puzzle.name);
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


    #region UI function
    public void SwitchMouse(Component sender, object data)
    {
        string mouseType = (string)data;
        Vector2 hotspot = new Vector2(-0.5f, 0.5f);


        switch (mouseType)
        {
            case "teleport":
                Cursor.SetCursor(teleportCursor, hotspot, CursorMode.Auto);
                break;

            case "item":
                Cursor.SetCursor(itemCursor, hotspot, CursorMode.Auto);
                break;

            default:
                Cursor.SetCursor(defCursor, hotspot, CursorMode.Auto);
                break;
        }

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

    public int GetSelectedItem()
    {
        return selectedItem;
    }

    public void SetSelectedItem(Component sender, object data)
    {
        selectedItem = (int)data;
    }

    public void GotRecipe(Component sender, object data)
    {
        getRecipeCanvas.SetActive(true);
        storyEvents.gotRecipe = true;
        StartCoroutine("TurnOffGotRecipe");
    }

    IEnumerator TurnOffGotRecipe()
    {
        yield return new WaitForSeconds(getRecipeAnim.clip.length);
        recipeCutsceneImage.SetActive(false);
        recipeTextImage.SetActive(true);
        getRecipeCanvas.SetActive(false);
    }

    public void ToggleRecipeCanvas(){
        recipeCanvasOpen = !recipeCanvasOpen;
        recipeCanvas.SetActive(recipeCanvasOpen);
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
