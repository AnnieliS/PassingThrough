using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerActions : MonoBehaviour
{
    #region events
    [Header("Events")]
    [SerializeField] GameEvent onDoubleClick;
    [Header("Dialogue Events")]
    [SerializeField] GameEvent startDialogue;
    [SerializeField] GameEvent continueDialogue;
    [SerializeField] GameEvent stopDialogue;
    [Header("Movement Events")]
    [SerializeField] GameEvent pausePlayerMovement;
    [SerializeField] GameEvent resumePlayerMovement;
    [SerializeField] GameEvent resetMousePos;
    [Header("Items Events")]
    [SerializeField] GameEvent tvChannelSurf;
    [SerializeField] GameEvent puzzle;
    [SerializeField] GameEvent tap;
    #endregion


    [Header("Params")]
    [SerializeField] float teleportShowTime = 0.2f;
    [SerializeField] float dialogueContinueTime = 0.5f;
    #region misc
    [Header("Misc")]
    [SerializeField] GameObject teleportCanvas;
    [SerializeField] GameObject teleportBack;
    #endregion

    #region dialogue params
    bool canPressContinue = true;
    bool insideDialogue = false;

    #endregion
    string clickTag = "";

    private GameObject tempGameObj;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Debug.Log(clickTag);
    }
    void OnInteract()
    {

        DialogueInteraction();
        Teleport();
        ItemPickup();
        UseItem();
        TVClick();
        Puzzle();
        Tap();
    }

    void OnSingleClick()
    {
        DialogueContinue();
    }

    #region collision functions

    private void OnTriggerEnter2D(Collider2D other)
    {
        // // Debug.Log("enter " + other.tag);
        // clickTag = other.tag;
        // tempGameObj = other.gameObject;
        if (clickTag == "ui")
        {
            pausePlayerMovement.Raise(this, tempGameObj);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("exit " + other.tag);
        if (other.tag == "ui")
        {
            resumePlayerMovement.Raise(this, "");
        }
        // clickTag = "";

    }

    #endregion

    #region Dialogue Function
    private void DialogueInteraction()
    {
        if (clickTag == "dialogue" && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            // Debug.Log("start dialogue player action");
            pausePlayerMovement.Raise(this, "");
            StartCoroutine(ResetPress());
            startDialogue.Raise(this, "dialogue");
        }



    }

    private void DialogueContinue()
    {
        if (canPressContinue)
        {
            // Debug.Log("continue");
            canPressContinue = false;
            continueDialogue.Raise(this, clickTag);
            StartCoroutine(ResetPress());
        }
    }

    void OnStopDia()
    {
        if (insideDialogue)
        {
            stopDialogue.Raise(this, "");
            clickTag = "";
        }
    }


    private IEnumerator ResetPress()
    {
        yield return new WaitForSeconds(dialogueContinueTime);
        canPressContinue = true;
    }

    public void DisableDialogueBool()
    {
        insideDialogue = false;
        clickTag = "";
    }

    #endregion

    #region teleportation functions

    private void Teleport()
    {
        if (clickTag == "teleport")
        {
            Vector3 newTeleportPosition = tempGameObj.GetComponent<Teleporter>().destinationPoint.transform.position;
            teleportCanvas.SetActive(true);
            teleportBack.SetActive(true);


            spriteRenderer.enabled = false;
            if (tempGameObj.tag == "teleport")
            {
                Sprite image = tempGameObj.GetComponent<Teleporter>().teleportImage;
                tempGameObj.GetComponent<AudioSource>().Play();
                FadeInAndOut fade = teleportCanvas.GetComponent<FadeInAndOut>();
                fade.Activate(image);
                fade.Activate2();
            }
            pausePlayerMovement.Raise(this, "");
            resetMousePos.Raise(this, newTeleportPosition);
            StartCoroutine(SpriteShowDelay(teleportShowTime, newTeleportPosition));
        }
    }

    private IEnumerator SpriteShowDelay(float time, Vector3 newTeleportPosition)
    {
        yield return new WaitForSeconds(time);
        resumePlayerMovement.Raise(this, "");
        spriteRenderer.enabled = true;
        gameObject.transform.position = newTeleportPosition;
        // teleportBack.SetActive(false);
    }

    #endregion

    #region items functions


    void ItemPickup()
    {
        if (clickTag == "item")
        {
            tempGameObj.GetComponent<CollectibleItem>().PickupItem();
        }
    }

    void UseItem()
    {
        if (clickTag == "itemUse")
        {
            tempGameObj.GetComponent<UseItem>().ClickedPlaceToUse();
        }
    }

    #endregion

    #region other functions

    void TVClick()
    {
        if (clickTag == "tv")
        {
            tvChannelSurf.Raise(this, "");
        }
    }

    void Puzzle()
    {
        if (clickTag == "puzzle")
        {
            puzzle.Raise(this, tempGameObj);
            clickTag = "";

        }
    }

    void Tap(){
        if(clickTag == "oil"){
            tap.Raise(this, "");
            clickTag = "";
        }
    }

    void End(){
        if(clickTag == "door"){
            GameManager.GetInstance().EndCutscene();
        }
    }

    public void DetectOverObject(Component sender, object data)
    {
        string tmptag = (string)data;
        clickTag = tmptag;
        tempGameObj = sender.gameObject;
    }

    #endregion

}
