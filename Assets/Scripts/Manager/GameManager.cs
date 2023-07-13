using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{

    #region Canvases
    [SerializeField] GameObject quitCanvas;
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] GameObject inventoryButtonCanvas;
    [SerializeField] GameObject recipeCanvas;
    [SerializeField] GameObject endCanvas;
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
    [SerializeField] Texture2D speakCursor;
    [SerializeField] Texture2D puzzleCursor;
    [SerializeField] Texture2D tvCursor;
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
    [SerializeField] GameEvent resumePlayer;
    [SerializeField] GameEvent pickUpItem;
    #endregion
    private static GameManager instance;

    #region Recipe
    [Header("recipe")]
    [SerializeField] GameObject recipeButton;
    [SerializeField] GameObject recipeCutsceneImage;
    [SerializeField] GameObject recipeTextImage;

    [SerializeField] GameEvent itemToPot;

    #endregion

    #region generalItems
    [Header("Tap")]
    [SerializeField] SpriteRenderer oilTap;
    [SerializeField] private TextAsset tapInk;
    [SerializeField] TextAsset oilInk;
    bool isTapOn = false;
    [Header("Collectible")]
    [SerializeField] CollectibleItem meltedLead;
    [SerializeField] CollectibleItem oilCup;

    [SerializeField] CollectibleItem key;
    #endregion

    #region story events
    private bool gotRecipe = false; // event 1
    private bool isChairInPlace = false; // event 2
    private bool gotOil = false; // event 3
    private bool putDirt = false; // event 4
    private bool putwater = false; // event 5
    private bool putoil = false; // event 6
    private bool putmatches = false; // event 7
    private bool putlead = false; // event 8
    private bool changesLocks = false;

    [SerializeField] GameObject keyAnim;
    [SerializeField] GameObject doorCollider;
    [SerializeField] GameObject newDoorCollider;
    [SerializeField] VideoPlayer endScenePlayer;


    #endregion

    #region init params
    bool quitCanvasOpen = false;
    bool recipeCanvasOpen = false;
    Animator getRecipeAnim;

    private int selectedItem;
    #endregion

    #region inits + unity funcs
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
        // StartingDialogue();
        ResetRecipe();
        Cursor.SetCursor(defCursor, new Vector2(-0.5f, 0.5f), CursorMode.Auto);

    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    private void Update()
    {
        if (putoil && putDirt && putwater && putlead && putmatches && !changesLocks)
        {
            changesLocks = true;
            FinishGame();
        }
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

    void ResetRecipe()
    {
        getRecipeAnim = recipeCanvas.GetComponentInChildren<Animator>();
        recipeCutsceneImage.SetActive(true);
        recipeTextImage.SetActive(false);
        recipeCanvas.SetActive(false);
        recipeButton.SetActive(false);
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

            case "itemUse":
                Cursor.SetCursor(speakCursor, hotspot, CursorMode.Auto);
                break;
            case "puzzle":
                Cursor.SetCursor(puzzleCursor, hotspot, CursorMode.Auto);
                break;

            case "dialogue":
                Cursor.SetCursor(speakCursor, hotspot, CursorMode.Auto);
                break;

            case "tv":
                Cursor.SetCursor(tvCursor, hotspot, CursorMode.Auto);
                break;

            case "oil":
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

    public void SetSelectedItem(int data)
    {
        selectedItem = data;
    }

    public void GotRecipe()
    {
        // Debug.Log("anim lenght : " + getRecipeAnim.GetCurrentAnimatorStateInfo(1).);
        Cursor.visible = false;
        recipeCanvas.SetActive(true);
        recipeCutsceneImage.GetComponent<AudioSource>().Play();
        EventFulfill(1);
        recipeButton.SetActive(true);
        pausePlayer.Raise(this, "");
        StartCoroutine("TurnOffGotRecipe");
    }

    IEnumerator TurnOffGotRecipe()
    {
        yield return new WaitForSeconds(6f);
        Cursor.visible = true;
        resumePlayer.Raise(this, "");
        recipeCutsceneImage.SetActive(false);
        recipeTextImage.SetActive(true);
        recipeCanvas.SetActive(false);
    }

    public void ToggleRecipeCanvas()
    {
        recipeCanvasOpen = !recipeCanvasOpen;
        recipeCanvas.SetActive(recipeCanvasOpen);
    }

    public void ToggleTap()
    {
        isTapOn = !isTapOn;
        oilTap.enabled = isTapOn;
        if (isTapOn)
        {
            dialogueStart.Raise(this, "dialogue");
            DialogueManager.GetInstance().EnterDialogueMode(tapInk);
            if (CheckPrecondition(1) && !CheckPrecondition(3))
            {
                dialogueStart.Raise(this, "dialogue");
                DialogueManager.GetInstance().EnterDialogueMode(oilInk);
                EventFulfill(3);
                pickUpItem.Raise(oilCup, "");
            }
        }
    }

    public bool GetTapState()
    {
        return isTapOn;
    }

    public void ItemToPot(int item)
    {
        itemToPot.Raise(this, item);
        item = -1;
    }

    public void MeltLead()
    {
        pickUpItem.Raise(meltedLead, "");
    }



    #endregion

    #region  story event functions
    public bool CheckPrecondition(int preconditionID)
    {

        switch (preconditionID)
        {
            case 1:
                return gotRecipe;
            case 2:
                return isChairInPlace;
            case 3:
                return gotOil;
            case 4:
                return putDirt;
            case 5:
                return putwater;
            case 6:
                return putoil;
            case 7:
                return putmatches;
            case 8:
                return putlead;


            default:
                return false;
        }

    }

    public void EventFulfill(int eventID)
    {
        switch (eventID)
        {
            case 1:
                gotRecipe = true;
                break;
            case 2:
                isChairInPlace = true;
                break;
            case 3:
                gotOil = true;
                break;
            case 4:
                putDirt = true;
                break;
            case 5:
                putwater = true;
                break;
            case 6:
                putoil = true;
                break;
            case 7:
                putmatches = true;
                break;
            case 8:
                putlead = true;
                break;
            default:
                break;
        }


    }


    void FinishGame()
    {
        doorCollider.SetActive(false);
        newDoorCollider.SetActive(true);
        StartCoroutine("GetKey");
    }

    IEnumerator GetKey()
    {
        yield return new WaitForSeconds(4f);
        keyAnim.SetActive(true);
        StartCoroutine("GetGetKey");

    }

    IEnumerator GetGetKey(){
        yield return new WaitForSeconds(3f);
        pickUpItem.Raise(key, "");
        keyAnim.SetActive(false);

    }

    public void EndCutscene()
    {
        Cursor.visible = false;
        endScenePlayer.Play();
        endCanvas.SetActive(true);

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
